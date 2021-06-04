using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //Declare states.
    public PlayerStateIdle playerStateIdle;
    public PlayerStateWalking playerStateWalking;
    public PlayerStateAttacking playerStateAttacking;

    //Components. More than one state uses component? Make public. Otherwise, make private and pass into updatecomponenents method as parameter.
    //Public Components.
    public PlayerAnimationController playerAnimationController { get; private set; }
    public Transform myTransform { get { return transform; } }
    //Private Components.
    private CharacterController characterController;

    private void Awake()
    {
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Start()
    {
        //Update components.
        playerStateIdle.UpdateComponents(this);
        playerStateWalking.UpdateComponents(this, characterController);
        playerStateAttacking.UpdateComponents(this);

        ChangeState(playerStateIdle);
    }
}
