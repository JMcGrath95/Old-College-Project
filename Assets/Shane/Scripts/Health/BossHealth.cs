using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public static Action<BossHealth> BossHealthInitialisedEvent;

    public override void Start()
    {
        base.Start();

        BossHealthInitialisedEvent?.Invoke(this);
    }

    public override void AddHealth(int amount) => base.AddHealth(amount);

    public override void AddMaxHealth(int amount) => base.AddMaxHealth(amount);

    public override void TakeDamage(int amount) => base.TakeDamage(amount);

    public override void SetMaxHealth(int amount) => base.SetMaxHealth(amount);
}
