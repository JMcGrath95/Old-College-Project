using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override event Action DeathEvent;
    public override event Action DamageTakenEvent;
    public override event Action HealthAddedEvent;
    public override event Action MaxHealthChangedEvent;

    public override void Start()
    {
        base.Start();

        PlayerStateDashing.DashStartedEvent += OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent += OnPlayerEndedDash;
    }

    private void OnPlayerEndedDash(float dashCooldown) => canTakeDamage = true;
    private void OnPlayerStartedDash() => canTakeDamage = false;

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

        currentHealth -= amount;
        DamageTakenEvent?.Invoke();

        if(currentHealth <= 0)
        {
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerStateDashing.DashStartedEvent -= OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }
}
