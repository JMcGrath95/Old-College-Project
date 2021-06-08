using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateWalking : iState
{
    PlayerStateMachine playerStateMachine;

    //Movement.
    CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool IsGrounded;
    [Header("Rotation Speed")]
    [SerializeField] private float rotationSpeed;
       
    public void UpdateComponents(PlayerStateMachine playerStateMachine,CharacterController characterController)
    {
        this.playerStateMachine = playerStateMachine;
        this.characterController = characterController;
    }

    public void Enter()
    {
        InputManager.AttackInputEvent += OnAttackInputEvent;
        InputManager.DashInputEvent += OnDashInputEvent;
    }

    public void Exit()
    {
        InputManager.AttackInputEvent -= OnAttackInputEvent;
        InputManager.DashInputEvent -= OnDashInputEvent;
    }

    public void Tick()
    {
        if (!InputManager.IsMovementInput)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerStateIdle);
            return;
        }

        playerStateMachine.playerAnimationController.GoToWalking();

        IsGrounded = characterController.SimpleMove(InputManager.MovementInput * movementSpeed);
        playerStateMachine.myTransform.rotation = Quaternion.Slerp(playerStateMachine.myTransform.rotation, Quaternion.LookRotation(InputManager.MovementInput), rotationSpeed);

    }
    private void OnAttackInputEvent() => playerStateMachine.ChangeState(playerStateMachine.playerStateAttacking);
    private void OnDashInputEvent() => playerStateMachine.ChangeState(playerStateMachine.playerStateDashing);


}
