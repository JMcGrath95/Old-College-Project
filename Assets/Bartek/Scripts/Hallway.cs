using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway : MonoBehaviour
{
    public GameObject door;
    public int direction;

    public void OpenDoor()
    {
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 20, door.transform.position.z);
    }
}
