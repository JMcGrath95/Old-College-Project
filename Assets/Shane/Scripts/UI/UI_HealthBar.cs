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

    protected virtual void Start()
    {
        if(healthEntity == null)
        {
            Debug.LogError($"Health bar object called - {gameObject.name} can't find health component to represent. Find health component before calling base.start in inheriting script.");
            return;
        }

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

    private void OnDestroy()
    {
        if(healthEntity)
        {
            healthEntity.DamageTakenEvent -= OnMyEntityHealthChanged;
            healthEntity.HealthAddedEvent -= OnMyEntityHealthChanged;
            healthEntity.DeathEvent -= OnMyHealthEntityDeath;
            healthEntity.MaxHealthChangedEvent -= OnMyEntityMaxHealthChanged;
        }
    }
}
