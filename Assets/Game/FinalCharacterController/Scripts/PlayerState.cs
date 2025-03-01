using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementState currentPlayerMovementState {get; private set;} = PlayerMovementState.Idling;
    public enum PlayerMovementState
    {
        Idling=0,
        Walking=1,
        Running=2,
        Sprinting=3,
        Jumping=4,
        Falling=5,
        Strafing=6

        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
