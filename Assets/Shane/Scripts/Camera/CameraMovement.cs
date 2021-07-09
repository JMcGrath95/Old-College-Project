using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private bool FollowPlayerAtStart;

    [Header("Player Info")]
    private Transform player;
    //private Vector3 offset;

    [Header("Camera Zoom")]
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float maxCameraZoom;
    private Camera mainCamera;

    private Vector3 playerLastPosition;

    
    private void Awake() { }
    private void Start()
    {
        mainCamera = Camera.main;

        if (FollowPlayerAtStart)
            FindPlayer();
    }

    private void Update()
    {
        //Camera Zoom.
        mainCamera.orthographicSize += Input.mouseScrollDelta.y * cameraZoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraZoom, maxCameraZoom);

        playerLastPosition = player.position;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        //transform.position = player.position + offset;

        transform.position += player.position - playerLastPosition;
    }    

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = transform.position - player.position;
    }
}