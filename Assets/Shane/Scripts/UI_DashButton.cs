using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DashButton : UI_ButtonWithCooldown
{
    public override void Awake() => base.Awake();

    private void Start()
    {
        PlayerStateDashing.DashEndedEvent += OnPlayerEndedDash;
    }

    private void OnPlayerEndedDash(float dashCooldown)
    {
        radialProgressBar.ResetProgressBar();
        radialProgressBar.StartFillingProgressBar(dashCooldown);
    }

    private void OnDestroy()
    {
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }
}
