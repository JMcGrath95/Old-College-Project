using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This item handles value changing effects and when they activate
/// you can stack multiple value changing effects 
/// </summary>

[CreateAssetMenu(fileName = "Value Changing Item", menuName = "Items/New Value Changing Item")]
public class ValueChangingItem : BaseItem
{
    public List<ValueChangingEffect> valueChangingEffects = new List<ValueChangingEffect>();

    public override void InitializeItem(GameObject owner)
    {
        Owner = owner;

        foreach (ValueChangingEffect effect in valueChangingEffects)
        {
            effect.InitializeEffect(Owner);
        }
    }

    public override void UseItem()
    {
        foreach (ValueChangingEffect effect in valueChangingEffects)
        {
            effect.TriggerEffect();
        }
    }

}
