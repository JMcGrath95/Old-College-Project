using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShop : InteractableArea
{
    public override void Start()
    {
        base.Start();

        FindObjectOfType<PlayerHealth>().DeathEvent += OnPlayerDeath;
    }
    private void OnPlayerDeath()
    {
        print("player died, oh no");
    }

    public override void Interact()
    {
        print("Interacted with shop");
        FindObjectOfType<PlayerHealth>().TakeDamage(50);
    }
}
