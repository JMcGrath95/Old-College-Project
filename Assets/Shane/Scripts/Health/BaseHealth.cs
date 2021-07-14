using System;
using System.Collections;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, iDamageable
{
    public abstract event Action DamageTakenEvent;
    public abstract event Action DeathEvent;
    public abstract event Action HealthAddedEvent;
    public abstract event Action MaxHealthChangedEvent;

    [Header("Health Stats")]
    [SerializeField] protected int currentHealth;
    [SerializeField] public int maxHealth;
    [Header("Invincibilty")]
    [SerializeField] public bool canTakeDamage;

    public virtual void Start() => currentHealth = maxHealth;

    public abstract void TakeDamage(int amount);
    public abstract void AddHealth(int amount);
    public virtual void SetMaxHealth(int amount) => maxHealth = amount;

    public virtual void AddMaxHealth(int amount) => maxHealth += amount;
    public int GetCurrentHealth => currentHealth;

    public void EnableInvincibilityForTimePeriod(float duration)
    {
        canTakeDamage = false;
        Timer timer = new Timer(this, duration, () => canTakeDamage = true);
        timer.Start();
    }
}