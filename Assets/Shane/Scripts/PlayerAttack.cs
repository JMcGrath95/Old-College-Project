using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private UnityAction CurrentAttack;

    [Header("Melee Attack")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float meleeAttackCooldown = 2f;

    private float timeOfLastAttack = float.MinValue;

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

        }
    }

    public void EnableWeaponHitbox() => currentWeapon.EnableAndDisableHitbox();
}
