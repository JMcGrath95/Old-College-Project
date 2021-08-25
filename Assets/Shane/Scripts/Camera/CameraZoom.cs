using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Camera Zoom Speed")]
    [SerializeField] private float cameraZoomSpeed;
    [Header("Camera Zoom Clamping")]
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float maxCameraZoom;
    [Header("Zoom at level generation")]
    [SerializeField] private float zoomAtLevelGeneration;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>(); 
    }

    private void Start()
    {
        GameController.GameStarted += OnGameStarted;

        mainCamera.orthographicSize = zoomAtLevelGeneration;
    }

    private void OnGameStarted()
    {
        mainCamera.orthographicSize = minCameraZoom;
    }

    private void Update()
    {
        if (PlayerStateMachine.PlayerControlState != PlayerControlState.InControl)
            return;

        //Camera Zoom.
        mainCamera.orthographicSize += -Input.mouseScrollDelta.y * cameraZoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraZoom, maxCameraZoom);
    }

    private void OnDestroy()
    {
        GameController.GameStarted -= OnGameStarted;
    }
}
