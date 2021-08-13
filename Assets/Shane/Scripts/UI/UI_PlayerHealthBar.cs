using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : UI_HealthBar
{

    private void Start()
    {
        GameController.GameStarted += OnGameStartedEvent;
    }

    private void OnGameStartedEvent()
    {
        healthEntity = FindObjectOfType<PlayerHealth>();
        UpdateHealthEntity(healthEntity);
    }

    protected override void OnMyHealthEntityDeath()
    {
        //Hide if player dies?
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GameController.GameStarted -= OnGameStartedEvent;
    }
}