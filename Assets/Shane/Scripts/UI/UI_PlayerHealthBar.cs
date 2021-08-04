using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : UI_HealthBar
{
    private void Start()
    {
        healthEntity = FindObjectOfType<PlayerHealth>();
        UpdateHealthEntity(healthEntity);
    }

    protected override void OnMyHealthEntityDeath()
    {
        //Hide if player dies?
    }
}