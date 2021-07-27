using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationController playerAnimationController;

    //Events.
    public static event Action PlayerAttackedEvent;

    [Header("Melee Attack")]
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] private float meleeAttackCooldown = 2f;
    [SerializeField] private float minMeleeAttackCooldown;
    [SerializeField] private float maxMeleeAttackCooldown;
    private UnityAction CurrentAttack;

    private float timeOfLastAttack = float.MinValue;

    //Attack Conditions
    public bool CanAttack { get { return AttackNotInCooldown;} }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;
    private void OnAttackSpeedModifierChanged(float percentageChange)
    {
        float newMeleeAttackCooldown = meleeAttackCooldown * (percentageChange * 0.01f);
        meleeAttackCooldown -= newMeleeAttackCooldown;

        meleeAttackCooldown = Mathf.Clamp(meleeAttackCooldown, minMeleeAttackCooldown, maxMeleeAttackCooldown);
    }


    //Start.
    private void Awake()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();

        PlayerAnimationController.AttackSpeedModifierChangedEvent += OnAttackSpeedModifierChanged;
    }


    private void Start() => CurrentAttack = MeleeAttack;

    public void MeleeAttack()
    {
        PlayerAttackedEvent?.Invoke();

        timeOfLastAttack = Time.time;

        //Snap rotation to input direction if any.
        if (InputManager.IsMovementInput)
            transform.forward = InputManager.MovementInput;

        playerAnimationController.GoToNextAttack();
    }

    public void SetTimeOfLastAttack() => timeOfLastAttack = Time.time; //Called at end of each attack animation through animation event.

    public void EnableWeaponHitbox() => currentWeapon.EnableHitbox();
    public void DisableWeaponHitbox() => currentWeapon.DisableHitbox();

    private void OnDestroy()
    {
        PlayerAnimationController.AttackSpeedModifierChangedEvent -= OnAttackSpeedModifierChanged;
    }
}
