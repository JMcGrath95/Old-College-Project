using System;

public class EnemyHealth : BaseHealth
{
    public override event Action DamageTakenEvent;
    public override event Action DeathEvent;
    public override event Action HealthAddedEvent;

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


    public override void TakeDamage(int amount)
    {
        if (!canTakeDamage)
            return;

        DamageTakenEvent?.Invoke();
        currentHealth -= amount;

        print($"Enemy hit, current health: {currentHealth}");

        if(currentHealth <= 0)
        {
            DeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
