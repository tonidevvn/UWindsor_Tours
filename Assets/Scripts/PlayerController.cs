using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] float directionBlendSpeed = 5f; // how fast to blend between directions

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;

    [SerializeField] Vector3 groundCheckOffset = new Vector3(0, 0.1f, 0);

    [SerializeField] LayerMask groundLayer;

    [Header("Jump Settings")]
    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float jumpButtonGracePeriod;

    Quaternion targetRotation;

    CameraController cameraController;

    Animator animator;

    CharacterController characterController;

    AudioManager audioManager;

    float? lastGroundedTime;

    float? jumpButtonPressedTime;

    bool isGrounded;

    bool isJumping;

    float ySpeed;

    private Vector3 cachedMoveDir = Vector3.zero;
    private Vector3 lastMoveInput = Vector3.zero;
    private bool isDirectionUpdatedAfterAlignment = false;

    float originalStepOffset;
    private bool isLanding; // Track if the player was falling to land

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        originalStepOffset = characterController.stepOffset;
        if (animator != null)
        {
            if (!animator.isInitialized)
            {
                animator.Rebind();
                animator.Update(0f);
                animator.SetFloat("MoveAmount", 0f);
                animator.SetBool("IsGrounded", true);
                animator.SetBool("IsJumping", false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // Returns a float value between -1 and 1
        float v = Input.GetAxis("Vertical"); // Returns a float value between -1 and 1
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveInput = new Vector3(h, 0, v).normalized;

        // Refresh or blend cached direction
        if (moveInput != lastMoveInput)
        {
            if (moveInput != Vector3.zero)
            {
                UpdateCachedMoveDir(moveInput); // New key pressed → refresh direction
            }
            else
            {
                cachedMoveDir = Vector3.zero;   // Key released → stop movement
            }

            lastMoveInput = moveInput;
        }

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (isLanding) // If the player was falling before touching the ground, play landing sound
            {
                audioManager.PlayJumpingLand();
                isLanding = false; // Reset flag after playing the sound
            }

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                audioManager.PlayJumpingStart();
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
                isLanding = true; // Set flag to play landing sound when the player touches the ground
            }
        }

        if (moveInput != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(cachedMoveDir, Vector3.up);

            float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            if (angleDiff > 1f)
            {
                float smoothFactor = Mathf.Clamp01((angleDiff / 180f) * 0.5f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothFactor * rotationSpeed * Time.deltaTime);
            }
        }

        if (isGrounded) {
            animator.SetFloat("MoveAmount", moveAmount, 0.2f, Time.deltaTime);
        }

        // Move player
        Vector3 velocity = cachedMoveDir * moveAmount * moveSpeed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        HandleWalkingRunningSound(moveAmount);
    }

    private void UpdateCachedMoveDir(Vector3 moveInput)
    {
        Vector3 camForward = cameraController.PlanarRotation * Vector3.forward;
        Vector3 camRight = cameraController.PlanarRotation * Vector3.right;
        cachedMoveDir = (camForward * moveInput.z + camRight * moveInput.x).normalized;
    }

    /// <summary>
    /// Correctly handles walking and running sounds without repeating them every frame.
    /// </summary>
    private void HandleWalkingRunningSound(float moveAmount)
    {
        if (isGrounded && moveAmount > 0)
        {
            if (moveAmount < 0.5f)
            {
                audioManager.PlayWalking();
            }
            else
            {
                audioManager.PlayRunning();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;
            characterController.Move(velocity);
        }
    }
}
