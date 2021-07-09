using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float rotationSpeed;

    private void Start() => player = GameObject.FindGameObjectWithTag("Player").transform;

    // Update is called once per frame
    void Update()
    {
        //Camera Rotation.
        transform.RotateAround(player.position,Vector3.up,Input.GetAxisRaw("Camera Rotation") * rotationSpeed);

    }
}
