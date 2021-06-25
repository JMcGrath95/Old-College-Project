using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private UnityAction CurrentAttack;

    [Header("Melee Attack")]
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] private float meleeAttackCooldown = 2f;

    private float timeOfLastAttack = float.MinValue;

    [Header("Attack Speed")]
    [SerializeField] private float startingAttackSpeed;

    //Attack Conditions
    private bool CanAttack { get { return AttackNotInCooldown;} }
    private bool AttackNotInCooldown => Time.time - timeOfLastAttack >= meleeAttackCooldown;

    //Start.
    private void Start()
    {
        CurrentAttack = MeleeAttack;
    }

    //Different potential attacks maybe.
    private void MeleeAttack()
    {
        if(CanAttack)
        {
            timeOfLastAttack = Time.time;

            //Snap rotation to input direction if any.
            if (InputManager.IsMovementInput)
                transform.forward = InputManager.MovementInput;


            //Need to rework attacking.
            //playerStateMachine.playerAnimationController.GoToNextAttack();
        }
    }

    public void EnableWeaponHitbox() => currentWeapon.EnableHitbox();
    public void DisableWeaponHitbox() => currentWeapon.DisableHitbox();
}
