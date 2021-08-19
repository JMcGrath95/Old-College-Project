using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    public Collider closedCollider;
    public Collider openLeftCollider;
    public Collider openRightCollider;

    public void OpenDoor()
    {
        animator.ResetTrigger("CloseDoor");
        animator.SetTrigger("OpenDoor");
        SetOpenDoorCollision();
    }

    public void CloseDoor()
    {
        animator.ResetTrigger("OpenDoor");
        animator.SetTrigger("CloseDoor");
        SetClosedDoorCollision();
    }

    void SetClosedDoorCollision()
    {
        closedCollider.enabled = true;
        openLeftCollider.enabled = false;
        openRightCollider.enabled = false;
    }

    void SetOpenDoorCollision()
    {
        closedCollider.enabled = false;
        openLeftCollider.enabled = true;
        openRightCollider.enabled = true;
    }
}
