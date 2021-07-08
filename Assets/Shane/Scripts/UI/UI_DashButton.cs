using UnityEngine;

//Dash button has progress bar which shows how long until dash cooldown ends.
//Listens out for player starting / ending dash.

public class UI_DashButton : UI_ButtonWithCooldown
{
    [SerializeField] private UI_ProgressBar progressBar;

    protected override void Awake() => base.Awake();

    private void Start()
    {
        PlayerStateDashing.DashStartedEvent += OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent += OnPlayerEndedDash;
    }

    private void OnPlayerStartedDash()
    {
        DisableButton();
        progressBar.ResetProgressBar();
    }

    private void OnPlayerEndedDash(float dashCooldown)
    {
        progressBar.StartFillingProgressBar(dashCooldown);

        EnableButtonCooldown(dashCooldown);
    }

    public void OnDestroy()
    {
        PlayerStateDashing.DashStartedEvent -= OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }
}