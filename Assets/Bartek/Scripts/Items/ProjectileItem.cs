using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This item handles projectile effects and when they activate
/// you can stack multiple projectile effects 
/// </summary>

[CreateAssetMenu(fileName = "Projectile Item", menuName = "Items/New Projectile Item")]
public class ProjectileItem : OffensiveItem
{
    public List<ProjectileEffect> ProjectileEffects = new List<ProjectileEffect>();

    public override void InitializeItem()
    {
        base.InitializeItem();
        foreach (ProjectileEffect pe in ProjectileEffects)
        {
            pe.InitializeEffect();
        }
    }

    public override void UseItem()
    {
        base.UseItem();

        foreach (ProjectileEffect pe in ProjectileEffects)
        {
            pe.TriggerEffect();
        }
    }
}
