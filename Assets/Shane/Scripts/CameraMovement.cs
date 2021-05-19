using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player Info")]
    private Transform player;
    private Vector3 startingPosition;
    private Vector3 amountPlayerMovedThisFrame;
    private Vector3 lastPlayerPosition;
    private Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startingPosition = transform.position;

        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }

}
