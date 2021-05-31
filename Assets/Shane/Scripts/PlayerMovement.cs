using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Attacking
}

public class PlayerMovement : MonoBehaviour
{
    //Components.
    CharacterController characterController;
    Camera mainCamera;
    PlayerAnimationController playerAnimationController;

    [Header("State")]
    public static PlayerState playerState;
    public static PlayerState previousState;

    [Header("Movement")]
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] private float movementSpeed;
    private Vector3 directionToMoveThisFrame;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;


    private bool IsGrounded;


    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        playerState = PlayerState.Idle;
        previousState = playerState;
    }

    private void Update()
    {
        UpdateMovementDirection();

        switch (playerState)
        {
            case PlayerState.Idle:

                playerAnimationController.GoToIdle(); 

                if (directionToMoveThisFrame != Vector3.zero)
                {
                    GoToNewState(PlayerState.Walking);
                }

                break;

            case PlayerState.Walking:

                if (directionToMoveThisFrame == Vector3.zero)
                {
                    GoToNewState(PlayerState.Idle);
                    return;
                }

                playerAnimationController.GoToWalking();

                IsGrounded = characterController.SimpleMove(directionToMoveThisFrame);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToMoveThisFrame), rotationSpeed);

                break;
            default:
                break;
        }
    }

    private void UpdateMovementDirection()
    {

#if UNITY_ANDROID || UNITY_IOS
        directionToMoveThisFrame = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);

#elif UNITY_STANDALONE
        directionToMoveThisFrame = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
#endif

        directionToMoveThisFrame = mainCamera.transform.TransformDirection(directionToMoveThisFrame) * Time.deltaTime;
        directionToMoveThisFrame = new Vector3(directionToMoveThisFrame.x, 0, directionToMoveThisFrame.z).normalized * movementSpeed;
    }

    public static void GoToNewState(PlayerState newState)
    {
        previousState = playerState;
        playerState = newState;
    }

    public void SnapRotationToInputDirection()
    {
        if(directionToMoveThisFrame != Vector3.zero)
            transform.forward = directionToMoveThisFrame;
    }
}

