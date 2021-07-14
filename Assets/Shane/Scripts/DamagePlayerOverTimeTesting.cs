using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOverTimeTesting : MonoBehaviour
{
    PlayerHealth playerHealth;
    
    void Start()
    {
         playerHealth = FindObjectOfType<PlayerHealth>();

         //playerHealth.SetMaxHealth(150);
         //InvokeRepeating("DamagePlayer", 2f, 3f);
    }

    private void DamagePlayer()
    {
        playerHealth.TakeDamage(30);
    }
}
