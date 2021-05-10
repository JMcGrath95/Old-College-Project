using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;



    private Vector3 startingPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startingPosition = transform.position;
    }

    private void LateUpdate()
    {
        transform.position += PlayerMovement.amountMovedThisFrame;
    }

}
