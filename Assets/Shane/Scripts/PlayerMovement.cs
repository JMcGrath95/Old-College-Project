using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
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


    //Animation transitions.
    private bool IsGrounded;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        animator.SetBool("Walking", true);
        //animator.setf
        //animator.Play("rig|Idle");
    }

    private void Update()
    {
        directionToMoveThisFrame = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        directionToMoveThisFrame = mainCamera.transform.TransformDirection(directionToMoveThisFrame);
        directionToMoveThisFrame = new Vector3(directionToMoveThisFrame.x, 0, directionToMoveThisFrame.z).normalized * movementSpeed;
        IsGrounded = characterController.SimpleMove(directionToMoveThisFrame);

        if (directionToMoveThisFrame != Vector3.zero)
        {
            transform.forward = directionToMoveThisFrame;
            print(IsGrounded);
        }
    }
}
