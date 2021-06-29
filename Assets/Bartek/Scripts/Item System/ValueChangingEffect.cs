using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This effect edits different values by the value that is passed into the scriptable object. 
/// The value is either added or subtracted based on a bool. 
/// </summary>

public enum ValueToChange
{
    Health,
    MovementSpeed,
    AttackSpeed,
    Damage,
    Money,
}

[CreateAssetMenu(fileName = "Value Changing Effect", menuName = "Effects/New Value Changing Effect")]
public class ValueChangingEffect : BaseEffect
{
    public ValueToChange valueToChange = ValueToChange.Health;              //The value this effect is editing
    public float Value = 10;                                                //Value that valueToChange gets changed by
    public bool Add = true;                                                 //Bool controlling whether Value gets added or subtracted
    public bool ForPlayer = true;                                           //Bool defining whether this effect edits player or enemy

    public override void InitializeEffect(GameObject owner)
    {
        Debug.Log(EffectName + " effect initiliazed.");
        Owner = owner;
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered.");

        if (Add) 
            AddValue();
        else 
            SubtractValue();
    }

    void AddValue()
    {
        switch (valueToChange)
        {
            case ValueToChange.Health:
                    Owner.GetComponent<BaseHealth>().AddHealth((int)Value);
                Debug.Log(Owner.name);
                break;

            case ValueToChange.MovementSpeed:
                if (ForPlayer)
                    PlayerStateWalking.MovementSpeed = Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.Speed += Value;
                break;

            case ValueToChange.AttackSpeed:
                if (ForPlayer)
                    Owner.GetComponentInChildren<PlayerAnimationController>().CurrentAttackSpeedModifier += Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.AttackSpeed += Value;
                break;

            case ValueToChange.Damage:
                if (ForPlayer)
                    Owner.GetComponent<PlayerAttack>().currentWeapon.Damage += (int)Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.Attack += Value;
                break;

            default:
                break;
        }
    }

    void SubtractValue()
    {
        switch (valueToChange)
        {
            case ValueToChange.Health:
                Owner.GetComponent<BaseHealth>().TakeDamage((int)Value);
                break;

            case ValueToChange.MovementSpeed:
                if (ForPlayer)
                    PlayerStateWalking.MovementSpeed -= Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.Speed -= Value;
                break;

            case ValueToChange.AttackSpeed:
                if (ForPlayer)
                    Owner.GetComponentInChildren<PlayerAnimationController>().CurrentAttackSpeedModifier -= Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.AttackSpeed -= Value;
                break;

            case ValueToChange.Damage:
                if (ForPlayer)
                    Owner.GetComponent<PlayerAttack>().currentWeapon.Damage -= (int)Value;
                else
                    Owner.GetComponent<Enemy_Stats>().Enemy.Attack -= Value;
                break;

            default:
                break;
        }
    }
}
