using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerStateAttacking : iState
{
    PlayerStateMachine playerStateMachine;

    private PlayerAttack playerAttack;

    public void UpdateComponents(PlayerStateMachine playerStateMachine,PlayerAttack playerAttack)
    {
        this.playerStateMachine = playerStateMachine;
        this.playerAttack = playerAttack;
    }

    public void Enter() 
    {
        if (!playerAttack.CanAttack)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerStateIdle);
            return;
        }
        else
            playerAttack.MeleeAttack();
    }

    public void Exit() { }
    public void Tick() { }
}
