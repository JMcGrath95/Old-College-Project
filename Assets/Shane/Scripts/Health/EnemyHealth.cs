using System;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public static event Action<EnemyHealth> EnemyDeathEvent;

    public override event Action DamageTakenEvent;
    public override event Action DeathEvent;
    public override event Action HealthAddedEvent;
    public override event Action MaxHealthChangedEvent;

    [Header("Points to give on Death")]
    [SerializeField] private int pointsReward;


    public override void Start()
    {
        base.Start();
        canTakeDamage = true;
    }

    public override void AddHealth(int amount)
    {
        currentHealth += amount;
        HealthAddedEvent?.Invoke();

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    public override void SetMaxHealth(int amount)
    {
        base.SetMaxHealth(amount);
        MaxHealthChangedEvent?.Invoke();
    }

    public override void TakeDamage(int amount)
    {
        if (!canTakeDamage)
            return;

        DamageTakenEvent?.Invoke();
        currentHealth -= amount;

        print($"Enemy hit, current health: {currentHealth}");

        if(currentHealth <= 0)
        {
            EnemyDeathEvent?.Invoke(this); //Static event.

            PlayerScoreController.PlayerScore += pointsReward;

            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
