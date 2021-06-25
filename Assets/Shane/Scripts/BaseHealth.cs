using System;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, iDamageable
{
    public abstract event Action DamageTakenEvent;
    public abstract event Action DeathEvent;
    public abstract event Action HealthAddedEvent;

    [Header("Health Stats")]
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [Header("Invincibilty")]
    [SerializeField] protected bool canTakeDamage;

    public virtual void Start() => currentHealth = maxHealth;

    public abstract void TakeDamage(int amount);
    public abstract void AddHealth(int amount);
    public virtual void SetMaxHealth(int amount) => maxHealth = amount;
    public virtual void AddMaxHealth(int amount) => maxHealth += amount;
    public int GetCurrentHealth => currentHealth;
    public void SetCanTakeDamage(bool canTakeDamage) => this.canTakeDamage = canTakeDamage;


    //IFrames.

}
