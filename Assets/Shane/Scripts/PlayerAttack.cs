using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;

    private Action CurrentAttack;

    [Header("Melee Attack Cooldown")]
    [SerializeField] private float meleeAttackCooldown = 2f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        CurrentAttack = MeleeAttack;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerMovement.playerState = PlayerState.Attacking;
            CurrentAttack();
        }
    }

    //Different potential attacks.
    private void MeleeAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        animator.SetBool("MeleeAttack1", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("MeleeAttack1", false);
        PlayerMovement.playerState = PlayerState.Attacking;

    }

}
