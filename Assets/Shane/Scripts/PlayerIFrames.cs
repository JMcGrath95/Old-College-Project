using System.Collections;
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

    private void EnableCollider() => playerCollider.enabled = true;
    private void DisableCollider() => playerCollider.enabled = false;

    //Turn off/on collider with duration.
    public void EnableIFrames(float duration) => StartCoroutine(EnableIFramesCoroutine(duration));
    private IEnumerator EnableIFramesCoroutine(float duration)
    {
        DisableCollider();
        yield return new WaitForSeconds(duration);
        EnableCollider();
    }


    private void OnPlayerStartedDash() => DisableCollider();
    private void OnPlayerEndedDash(float cooldown) => EnableCollider();

    private void OnDestroy()
    {
        PlayerStateDashing.DashStartedEvent -= OnPlayerStartedDash;
        PlayerStateDashing.DashEndedEvent -= OnPlayerEndedDash;
    }
}
