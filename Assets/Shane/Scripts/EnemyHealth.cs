using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public override event Action DamageTakenEvent;
    public override event Action DeathEvent;

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;


    }
}
