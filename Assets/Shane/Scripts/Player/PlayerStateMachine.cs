using UnityEngine;

public enum PlayerControlState
{
    InControl,
    Locked
}

public class PlayerStateMachine : StateMachine
{
    private static PlayerControlState playerControlState;
    public static PlayerControlState PlayerControlState { get { return playerControlState; } set { playerControlState = value; } }

    //Declare states.
    public PlayerStateIdle playerStateIdle;
    public PlayerStateWalking playerStateWalking;
    public PlayerStateAttacking playerStateAttacking;
    public PlayerStateDashing playerStateDashing;

    //Components. More than one state uses component? Make public. Otherwise, make private and pass into updatecomponenents method as parameter.
    //Public Components.
    public PlayerAnimationController playerAnimationController { get; private set; }
    public Transform myTransform { get { return transform; } }
    //Private Components.
    private CharacterController characterController;
    private PlayerAttack playerAttack;

    public GameObject lantern;
    bool switched = false;

    private void Awake()
    {
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        characterController = GetComponentInParent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();

        UI_PauseScreenController.GamePausedEvent += OnGamePaused;
        UI_PauseScreenController.GameUnpausedEvent += OnGameUnpaused;
    }

    private void OnGameUnpaused()
    {
        PlayerControlState = PlayerControlState.InControl;
    }

    private void OnGamePaused()
    {
        PlayerControlState = PlayerControlState.Locked;
    }

    private void Start()
    {
        //Update components.
        playerStateIdle.UpdateComponents(this);
        playerStateWalking.UpdateComponents(this, characterController);
        playerStateAttacking.UpdateComponents(this, playerAttack);
        playerStateDashing.UpdateComponents(this, characterController);

        PlayerControlState = PlayerControlState.InControl;

        ChangeState(playerStateIdle);
    }

    //public static void ChangePlayerControlState(PlayerControlState newState) => playerControlState = newState;

    public override void Update()
    {
        if (playerControlState == PlayerControlState.InControl)
        {
            base.Update();
        }

        if (currentState == playerStateIdle && switched)
        {
            switched = false;
            lantern.transform.localRotation = Quaternion.Euler(0, 85, -180);
        }

        if (currentState == playerStateWalking && !switched)
        {
            switched = true;
            lantern.transform.localRotation = Quaternion.Euler(lantern.transform.rotation.x, lantern.transform.rotation.y, lantern.transform.rotation.z - 90);
        }
    }

    private void OnDestroy()
    {
        UI_PauseScreenController.GamePausedEvent -= OnGamePaused;
        UI_PauseScreenController.GameUnpausedEvent -= OnGameUnpaused;
    }
}
