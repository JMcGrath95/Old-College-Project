using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Health bar UI base script.
public class UI_HealthBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Slider healthBarSlider;

    [Header("Object with health to display")]
    protected BaseHealth healthEntity;


    protected void UpdateHealthEntity(BaseHealth healthEntity)
    {
        this.healthEntity = healthEntity;

        healthBarSlider.maxValue = healthEntity.maxHealth;
        healthBarSlider.value = healthEntity.maxHealth;

        healthEntity.DamageTakenEvent += OnMyEntityHealthChanged;
        healthEntity.HealthAddedEvent += OnMyEntityHealthChanged;
        healthEntity.DeathEvent += OnMyHealthEntityDeath;
        healthEntity.MaxHealthChangedEvent += OnMyEntityMaxHealthChanged;
    }

    //Reactions to health entity events.
    private void OnMyEntityMaxHealthChanged() => healthBarSlider.maxValue = healthEntity.maxHealth;
    protected virtual void OnMyEntityHealthChanged() => healthBarSlider.value = healthEntity.GetCurrentHealth;
    protected virtual void OnMyHealthEntityDeath() { }

    public virtual void OnDestroy()
    {
        StopListeningToHealthEntityEvents();
    }

    protected void StopListeningToHealthEntityEvents()
    {
        if (healthEntity)
        {
            healthEntity.DamageTakenEvent -= OnMyEntityHealthChanged;
            healthEntity.HealthAddedEvent -= OnMyEntityHealthChanged;
            healthEntity.DeathEvent -= OnMyHealthEntityDeath;
            healthEntity.MaxHealthChangedEvent -= OnMyEntityMaxHealthChanged;
        }
    }
}
