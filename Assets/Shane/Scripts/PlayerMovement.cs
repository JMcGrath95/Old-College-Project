using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    //Components.
    Camera mainCamera;
    CharacterController characterController;
    Animator animator;

    [Header("Speed")]
    [SerializeField] private float movementSpeed = 10f;

    private Vector3 directionToMoveThisFrame;
    private Vector3 lastMoveDirection;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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


        if (directionToMoveThisFrame != Vector3.zero)
        {
            characterController.SimpleMove(directionToMoveThisFrame);
            transform.forward = directionToMoveThisFrame;
        }
    }
}
