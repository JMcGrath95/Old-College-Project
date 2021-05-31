using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, iDamageable
{
    public abstract event Action DamageTakenEvent;
    public abstract event Action DeathEvent;

    [SerializeField] protected int startingHealth;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;

    public virtual void Start() => currentHealth = startingHealth;

    public abstract void TakeDamage(int amount);

    public virtual void AddHealth(int amount) 
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }
}
