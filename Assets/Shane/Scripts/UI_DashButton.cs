using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DashButton : UI_ButtonWithCooldown
{
    protected override void Awake() => base.Awake();

    protected override void Start()
    {
        PlayerStateDashing.DashStartedEvent += OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent += OnPlayerEndedDash;
    }

    private void OnPlayerStartedDash() => DisableButton();

    private void OnPlayerEndedDash(float dashCooldown)
    {
        progressBar.ResetProgressBar();
        progressBar.StartFillingProgressBar(dashCooldown);

        EnableButtonCooldown(dashCooldown);
    }

    public void OnDestroy()
    {
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }
}
