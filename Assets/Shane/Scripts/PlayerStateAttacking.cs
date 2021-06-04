using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Add reference to player attack script?

[Serializable]
public class PlayerStateAttacking : iState
{
    PlayerStateMachine playerStateMachine;

    [Header("Weapon Info")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float meleeAttackCooldown = 2f;

    private float timeOfLastAttack = float.MinValue;

    //Attack Conditions
    private bool CanAttack { get { return AttackNotInCooldown; } }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;


    public void UpdateComponents(PlayerStateMachine playerStateMachine) => this.playerStateMachine = playerStateMachine;

    public void Enter() 
    {
        if(!CanAttack)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerStateIdle);
            return;
        }
        else
        {
            timeOfLastAttack = Time.time;

            if (InputManager.IsMovementInput)
                playerStateMachine.myTransform.forward = InputManager.MovementInput;


            //Need to rework attacking.
            playerStateMachine.playerAnimationController.GoToAttacking();

        }
    }

    public void Exit() { }
    public void Tick() { }

    public void EnableWeaponHitbox() => currentWeapon.EnableAndDisableHitbox();
}
