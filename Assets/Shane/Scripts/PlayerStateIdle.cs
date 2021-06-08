using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateIdle : iState
{
    PlayerStateMachine playerStateMachine;

    public void UpdateComponents(PlayerStateMachine playerStateMachine) => this.playerStateMachine = playerStateMachine;

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

    private void OnAttackInputEvent() => playerStateMachine.ChangeState(playerStateMachine.playerStateAttacking);
    private void OnDashInputEvent() => playerStateMachine.ChangeState(playerStateMachine.playerStateDashing);


    public void Tick()
    {
        playerStateMachine.playerAnimationController.GoToIdle();

        if (InputManager.IsMovementInput)
            playerStateMachine.ChangeState(playerStateMachine.playerStateWalking);
       
    }
}
