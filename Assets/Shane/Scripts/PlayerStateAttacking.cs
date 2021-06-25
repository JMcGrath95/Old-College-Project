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

    private PlayerAttack playerAttack;

    [Header("Weapon Info")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float meleeAttackCooldown = 2f;

    [Header("Attack Speed")]
    [SerializeField] private float startingAttackSpeed;

    private float timeOfLastAttack = float.MinValue;

    //Attack Conditions
    private bool CanAttack { get { return AttackNotInCooldown; } }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    public void UpdateComponents(PlayerStateMachine playerStateMachine,PlayerAttack playerAttack)
    {
        this.playerStateMachine = playerStateMachine;
        this.playerAttack = playerAttack;
    }

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

            //Snap rotation to input direction if any.
            if (InputManager.IsMovementInput)
                playerStateMachine.myTransform.forward = InputManager.MovementInput;


            //Need to rework attacking.
            playerStateMachine.playerAnimationController.GoToNextAttack();
        }
    }

    public void Exit() { }
    public void Tick() { }

    public void EnableWeaponHitbox() => currentWeapon.EnableHitbox();
    public void DisableWeaponHitbox() => currentWeapon.DisableHitbox();
}
