using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float locoMotionBlendSpeed = 0.02f;


    private PlayerLocoMotionInput _playerLocoMotionInput;


    private static int inputXHash = Animator.StringToHash("InputX");
    private static int inputYHash = Animator.StringToHash("InputY");
    
    private Vector3 _currentBlendInput = Vector3.zero;

    private void Awake() {
        _playerLocoMotionInput = GetComponent<PlayerLocoMotionInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState();   
    }

    private void UpdateAnimationState() {
        Vector2 inputTarget = _playerLocoMotionInput.MovementInput;
        _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locoMotionBlendSpeed * Time.deltaTime);
        _animator.SetFloat(inputXHash, _currentBlendInput.x);
        _animator.SetFloat(inputYHash, _currentBlendInput.y);

    }
}
