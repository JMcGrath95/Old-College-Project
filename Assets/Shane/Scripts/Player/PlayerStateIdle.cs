using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateIdle : iState
{
    private PlayerStateMachine playerStateMachine;

    public void UpdateComponents(PlayerStateMachine playerStateMachine) => this.playerStateMachine = playerStateMachine;

    public void Enter() => InputManager.AttackInputEvent += OnAttackInputEvent;

    public void Exit() => InputManager.AttackInputEvent -= OnAttackInputEvent;

    private void OnAttackInputEvent() => playerStateMachine.ChangeState(playerStateMachine.playerStateAttacking);

    public void Tick()
    {
        playerStateMachine.playerAnimationController.GoToIdle();

        if (InputManager.IsMovementInput)
            playerStateMachine.ChangeState(playerStateMachine.playerStateWalking);
       
    }
}
