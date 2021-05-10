using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    Camera mainCamera;
    Rigidbody rb;

    public static Vector3 directionToMoveThisFrame { get; set; } = Vector3.zero;
    public static Vector3 amountMovedThisFrame { get; set; } = Vector3.zero;

    private Vector3 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        directionToMoveThisFrame = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        directionToMoveThisFrame = mainCamera.transform.TransformDirection(directionToMoveThisFrame);
        directionToMoveThisFrame = new Vector3(directionToMoveThisFrame.x, 0, directionToMoveThisFrame.z).normalized * movementSpeed;

        //rb.MovePosition(transform.position + directionToMoveThisFrame);
        rb.velocity = directionToMoveThisFrame;

        amountMovedThisFrame = transform.position - lastPosition;

        lastPosition = transform.position;
    }




}
