using UnityEngine;

public enum PlayerControlState
{
    InControl,
    Locked
}

public class PlayerStateMachine : StateMachine
{
    private static PlayerControlState playerControlState = PlayerControlState.Locked;
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
    private PlayerHealth playerHealth;

    public GameObject lantern;
    bool switched = false;

    private void Awake()
    {
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        characterController = GetComponentInParent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponentInParent<PlayerHealth>();

        playerHealth.DeathEvent += LockPlayerControl;
        UI_PauseScreenController.GamePausedEvent += LockPlayerControl;
        UI_PauseScreenController.GameUnpausedEvent += EnablePlayerControl;
    }

    private void EnablePlayerControl()
    {
        PlayerControlState = PlayerControlState.InControl;
    }

    private void LockPlayerControl()
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
        playerHealth.DeathEvent -= LockPlayerControl;
        UI_PauseScreenController.GamePausedEvent -= LockPlayerControl;
        UI_PauseScreenController.GameUnpausedEvent -= EnablePlayerControl;
    }
}
