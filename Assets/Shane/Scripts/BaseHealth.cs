using System;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, iDamageable
{
    public abstract event Action DamageTakenEvent;
    public abstract event Action DeathEvent;
    public abstract event Action HealthAddedEvent;

    
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;

    public virtual void Start() => currentHealth = maxHealth;

    //Should probably make virtual. 
    public abstract void TakeDamage(int amount);
    public abstract void AddHealth(int amount);
    public virtual void SetMaxHealth(int amount) => maxHealth = amount;
    public virtual void AddMaxHealth(int amount) => maxHealth += amount;

    public virtual int GetCurrentHealth()
    {
        return currentHealth;
    }
}
