using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override event Action DeathEvent;
    public override event Action DamageTakenEvent;

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;
        DamageTakenEvent?.Invoke();

        if(currentHealth <= 0)
        {
            DeathEvent?.Invoke();
        }       
    }
}
