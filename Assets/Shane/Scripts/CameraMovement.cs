using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player Info")]
    private Transform player;
    private Vector3 offset;

    private void Awake() { }
    private void Start() { }

    private void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = player.position + offset;
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }
}
