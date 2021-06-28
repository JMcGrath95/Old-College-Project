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
    private UnityAction CurrentAttack;

    private float timeOfLastAttack = float.MinValue;

    //Attack Conditions
    public bool CanAttack { get { return AttackNotInCooldown;} }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    //Start.
    private void Awake() => playerAnimationController = GetComponent<PlayerAnimationController>();
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

    public void EnableWeaponHitbox() => currentWeapon.EnableHitbox();
    public void DisableWeaponHitbox() => currentWeapon.DisableHitbox();
}
