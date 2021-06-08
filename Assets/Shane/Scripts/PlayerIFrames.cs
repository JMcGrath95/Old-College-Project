using UnityEngine;

//Turns on/off player collider during a dash.
//Disabling outer trigger collider of player not the character controller collider.
public class PlayerIFrames : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;

    private void Start()
    {
        PlayerStateDashing.DashStartedEvent += OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent += OnPlayerEndedDash;
    }

    private void OnPlayerStartedDash() => playerCollider.enabled = false;
    private void OnPlayerEndedDash(float cooldown) => playerCollider.enabled = true;

    private void OnDestroy()
    {
        PlayerStateDashing.DashStartedEvent -= OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }

}
