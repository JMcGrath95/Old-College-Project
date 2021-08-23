using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This item handles projectile effects and when they activate
/// you can stack multiple projectile effects 
/// </summary>

[CreateAssetMenu(fileName = "Projectile Item", menuName = "Items/New Projectile Item")]
public class ProjectileItem : BaseItem
{
    public List<ProjectileEffect> ProjectileEffects = new List<ProjectileEffect>();

    public override void InitializeItem(GameObject owner)
    {
        Owner = owner;

        foreach (ProjectileEffect effect in ProjectileEffects)
        {
            effect.InitializeEffect(Owner);
        }

        UI_NotificationMessageController.Instance.ShowMessage($"Projectile added to attack");
    }

    public override void UseItem()
    {
        foreach (ProjectileEffect pe in ProjectileEffects)
        {
            pe.TriggerEffect();
        }
    }
}
