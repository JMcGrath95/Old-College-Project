using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAnimationController playerAnimationController;

    private Action CurrentAttack;

    [Header("Melee Attack Cooldown")]
    [SerializeField] private float meleeAttackCooldown = 2f;
    private float timeOfLastAttack = float.MinValue;
    private float meleeAttackAnimationTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }
    private void Start()
    {
        CurrentAttack = MeleeAttack;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && AttackNotInCooldown)
        {
            PlayerMovement.GoToNewState(PlayerState.Attacking);
            CurrentAttack();
        }
    }

    //Different potential attacks maybe.
    private void MeleeAttack()
    {
        //Find better way of getting animation time.
        //AnimatorStateInfo animatorStateInfo = playerMovement.animator.GetCurrentAnimatorStateInfo(0);
        timeOfLastAttack = Time.time;
        playerMovement.SnapForwardRotationToInputDirection();
        playerAnimationController.GoToAttacking(callAtEndOfAnimation: EndAttack);
    }

    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    private void EndAttack()
    {
        PlayerMovement.GoToNewState(PlayerState.Walking);
    }
}
