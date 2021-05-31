using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAnimationController playerAnimationController;

    private UnityAction CurrentAttack;

    [Header("Melee Attack")]
    [SerializeField] private Button btnAttack;
    [SerializeField] private float meleeAttackCooldown = 2f;
    private float timeOfLastAttack = float.MinValue;
    private float meleeAttackAnimationTime;
    private bool CanAttack { get { return AttackNotInCooldown && IsAttacking == false; } }
    private bool IsAttacking;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }
    private void Start()
    {
        CurrentAttack = MeleeAttack;
        btnAttack.onClick.AddListener(CurrentAttack);
    }

    //Different potential attacks maybe.
    private void MeleeAttack()
    {
        if(CanAttack)
        {
            PlayerMovement.GoToNewState(PlayerState.Attacking);
            IsAttacking = true;
            timeOfLastAttack = Time.time;

            playerMovement.SnapRotationToInputDirection();
            playerAnimationController.GoToAttacking();
        }
    }

    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    public void EndAttack()
    {
        PlayerMovement.GoToNewState(PlayerState.Walking);
        IsAttacking = false;
    }
}
