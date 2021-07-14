using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : UI_HealthBar
{
    protected override void Start()
    {
        healthEntity = FindObjectOfType<PlayerHealth>();
        base.Start();
    }

    protected override void OnMyHealthEntityDeath()
    {
        
    }
}