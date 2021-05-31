using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    //Components.
    PlayerMovement playerMovement;
    PlayerAnimationController playerAnimationController;

    private UnityAction CurrentAttack;

    [Header("Melee Attack")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Button btnAttack;
    [SerializeField] private float meleeAttackCooldown = 2f;

    private float timeOfLastAttack = float.MinValue;

    //Attack Conditions
    private bool CanAttack { get { return AttackNotInCooldown && PlayerMovement.playerState != PlayerState.Attacking; } }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    //Start.
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }
    private void Start()
    {
        CurrentAttack = MeleeAttack;

#if UNITY_ANDROID || UNITY_IOS
        btnAttack.onClick.AddListener(CurrentAttack);
#endif
    }

#if UNITY_STANDALONE

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CurrentAttack();
    }
#elif UNITY_ANDROID || UNITY_IOS

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CurrentAttack();
    }
#endif



    //Different potential attacks maybe.
    private void MeleeAttack()
    {
        if(CanAttack)
        {
            PlayerMovement.GoToNewState(PlayerState.Attacking);
            timeOfLastAttack = Time.time;

            playerMovement.SnapRotationToInputDirection();
            playerAnimationController.GoToAttacking();
        }
    }


    public void EndAttack() => PlayerMovement.GoToNewState(PlayerState.Walking);

    public void EnableWeaponHitbox() => currentWeapon.EnableAndDisableHitbox();
}
