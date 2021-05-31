using System;

public class PlayerHealth : BaseHealth
{
    public override event Action DeathEvent;
    public override event Action DamageTakenEvent;
    public override event Action HealthAddedEvent;

    public override void Start()
    {
        base.Start();

        PickableItemHealthPotion.PickedUpHealthPotionEvent += OnPickedUpHealthPotion;
    }

    private void OnPickedUpHealthPotion(string itemID, int healthToGive)
    {
        AddHealth(healthToGive);
        print($"Picked up health potion: current health is now {currentHealth}.");
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
        currentHealth -= amount;
        DamageTakenEvent?.Invoke();

        if(currentHealth <= 0)
            DeathEvent?.Invoke();     
    }


    private void OnDestroy() => PickableItemHealthPotion.PickedUpHealthPotionEvent -= OnPickedUpHealthPotion;

}
