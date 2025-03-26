using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] Vector3 followOffset = new Vector3(0f, 2.5f, 0f); // move camera up above the target

    [SerializeField] float rotationSpeed = 2f;

    [SerializeField] float distance = 5f;

    [SerializeField] float minVerticalAngle = 0f;
    [SerializeField] float maxVerticalAngle = 45f;

    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;

    private Vector3 currentVelocity;

    [SerializeField] bool invertX;

    [SerializeField] bool invertY;

    [SerializeField] float followRotationSpeed = 2f;

    [SerializeField] float followSmoothTime = 0.2f;

    [SerializeField] float rotationDamping = 8f; // Smooth rotation factor

    [SerializeField] float followDelay = 1.5f; // seconds before camera rotates behind player

    private CursorController cursorController;
    private float followTimer = 0f;
    private bool isDragging = false;
    private Vector3 mouseDownPosition;
    private float mouseDragThreshold = 2f; // pixels

    void Awake()
    {
        cursorController = FindFirstObjectByType<CursorController>();
    }

    void LateUpdate()
    {
        (invertXVal, invertYVal) = (invertX ? -1 : 1, invertY ? -1 : 1);

        // Get player movement input
        float horizontalInput = Input.GetAxis("Camera X");
        float verticalInput = Input.GetAxis("Camera Y");
        bool isMouseHeld = Input.GetMouseButton(0) || Input.GetMouseButton(2);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
        {
            mouseDownPosition = Input.mousePosition;
            isDragging = false;
        }

        if (isMouseHeld)
        {
            float dragDistance = (Input.mousePosition - mouseDownPosition).sqrMagnitude;
            if (!isDragging && dragDistance > mouseDragThreshold * mouseDragThreshold)
            {
                cursorController.HideCursor(); // Only hide and lock if actual dragging happens
                isDragging = true;
            }

            if (isDragging)
            {
                // Rotate camera with mouse movement
                rotationX += verticalInput * invertYVal * rotationSpeed;
                rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
                rotationY += horizontalInput * invertXVal * rotationSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(2))
        {
            cursorController.ShowCursor(); // Release lock on mouse up
            isDragging = false;
        }

        // Get player movement input
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");
        bool isPlayerMoving = Mathf.Abs(moveInputX) > 0.1f || Mathf.Abs(moveInputZ) > 0.1f;

        if (isPlayerMoving)
        {
            cursorController.HideCursor(); // Immediately hide when player moves
        }
        // Only rotate behind player when keys pressed

        // Check if the player is moving with keyboard (not mouse) and camera is idle
        bool isManualCameraMoving = Mathf.Abs(horizontalInput) > 0.01f || Mathf.Abs(verticalInput) > 0.01f;

        // Only start timer when moving but not adjusting camera manually
        if (isPlayerMoving && !isManualCameraMoving)
        {
            followTimer += Time.deltaTime;

            if (followTimer >= followDelay)
            {
                float targetY = followTarget.eulerAngles.y;
                float angle = Mathf.DeltaAngle(rotationY, targetY);

                if (Mathf.Abs(angle) > 0.1f)
                {
                    rotationY = Mathf.LerpAngle(rotationY, targetY, Time.deltaTime * followRotationSpeed);
                }
                else
                {
                    rotationY = targetY; // Snap exactly behind player when close enough
                }
            }
        }
        else
        {
            followTimer = 0f; // reset if no input or camera is moved
        }

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = followTarget.position + followOffset;
        var targetPosition = focusPosition - targetRotation * Vector3.forward * distance;

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }

    public bool IsAlignedBehindPlayer
    {
        get
        {
            float angle = Mathf.DeltaAngle(rotationY, followTarget.eulerAngles.y);
            return Mathf.Abs(angle) < 1f; // consider aligned if very close
        }
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
