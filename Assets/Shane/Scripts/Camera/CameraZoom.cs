using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Camera Zoom Speed")]
    [SerializeField] private float cameraZoomSpeed;
    [Header("Camera Zoom Clamping")]
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float maxCameraZoom;

    private void Start() => mainCamera = GetComponent<Camera>();

    private void Update()
    {
        if (PlayerStateMachine.PlayerControlState != PlayerControlState.InControl)
            return;

        //Camera Zoom.
        mainCamera.orthographicSize += -Input.mouseScrollDelta.y * cameraZoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraZoom, maxCameraZoom);

    }
}
