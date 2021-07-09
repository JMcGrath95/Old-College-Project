using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Transform player;


    private float RotationInput
    { 
        get 
        {
            if (Input.GetKey(KeyCode.Q))
                return 1;
            else if (Input.GetKey(KeyCode.E))
                return -1;

            return 0;     
        } 
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera Rotation.
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(player.position,Vector3.up,2f);
        }

    }
}
