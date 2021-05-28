using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, iDamageable
{
    public virtual event Action DamageTakenEvent;
    public virtual event Action DeathEvent;

    [SerializeField] protected int startingHealth;
    [SerializeField] protected int currentHealth;

    public virtual void Start() => currentHealth = startingHealth;

    public abstract void TakeDamage(int amount);
}
