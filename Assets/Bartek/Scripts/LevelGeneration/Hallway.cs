using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway : MonoBehaviour
{
    public Door door;

    //1 = up, 2 = right, 3 = down, 4 = left
    public int direction;

    public void OpenDoor()
    {
        door.OpenDoor();
    }

    public void CloseDoor()
    {
        door.CloseDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(GetComponentInParent<Room>().inRoomTrigger && !GetComponentInParent<Room>().roomEntered && !GetComponentInParent<Room>().roomCleared)
            {
                GetComponentInParent<Room>().StartRoom();
                GetComponentInParent<Room>().roomEntered = true;
            }
        }
    }
}
