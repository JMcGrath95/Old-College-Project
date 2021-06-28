using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway : MonoBehaviour
{
    public GameObject door;

    //1 = up, 2 = right, 3 = down, 4 = left
    public int direction;

    public void OpenDoor()
    {
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 70, door.transform.position.z);
    }

    public void CloseDoor()
    {
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 70, door.transform.position.z);
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
