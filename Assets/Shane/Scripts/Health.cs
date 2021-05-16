using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action DamageTakenEvent;
    public event Action DeathEvent;

    [SerializeField] private int startingHealth;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        DamageTakenEvent?.Invoke();

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            DeathEvent?.Invoke();
        }      
    }
}
