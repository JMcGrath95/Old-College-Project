using UnityEngine;

enum PlayerState
{
    Idle,
    Walking,
    Attacking
}

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Components.
    CharacterController characterController;
    Camera mainCamera;
    Animator animator;

    [Header("State")]
    [SerializeField] private PlayerState playerState;

    [Header("Movement")]
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] private float movementSpeed;
    private Vector3 directionToMoveThisFrame;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;


    private bool IsGrounded;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //Move.
        UpdateMovementDirection();
        IsGrounded = characterController.SimpleMove(directionToMoveThisFrame);

        //Update animator.
        animator.SetFloat("MovementSpeed", directionToMoveThisFrame.magnitude);

        //Idle.
        if (directionToMoveThisFrame == Vector3.zero)
        {
            playerState = PlayerState.Idle;

        }
        //Walking.
        else
        {
            playerState = PlayerState.Walking;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToMoveThisFrame), rotationSpeed);
        }
    }

    private void UpdateMovementDirection()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        directionToMoveThisFrame = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
#endif
#if UNITY_ANDROID || UNITY_IOS
        directionToMoveThisFrame = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);
#endif

        directionToMoveThisFrame = mainCamera.transform.TransformDirection(directionToMoveThisFrame);
        directionToMoveThisFrame = new Vector3(directionToMoveThisFrame.x, 0, directionToMoveThisFrame.z).normalized * movementSpeed * Time.deltaTime;
    }
}

