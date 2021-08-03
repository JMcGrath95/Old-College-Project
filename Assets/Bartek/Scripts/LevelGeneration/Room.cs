using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RoomType
{
    StartRoom,
    BossRoom,
    EnemyRoom,
    EmptyRoom,
    TreasureRoom,
    TrapRoom,
}

public class Room : MonoBehaviour
{
    public List<Hallway> hallways = new List<Hallway>();

    //public int roomType;
    public RoomType roomType;

    //floor object temporarily used for color changing to know what roomtype it is
    public GameObject floor;

    public bool inRoomTrigger = false, roomCleared = false, roomEntered = false;

    //bools to check if there is a hallway in the corresponding position
    public bool topHallway, rightHallway, bottomHallway, leftHallway;

    public Spawner spawner;

    RoomController controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("RoomController").GetComponent<RoomController>();
        roomCleared = false;
        BuildFloorNavMesh();
    }

    //loops through children and assigns floor object if it finds an object named Floor
    public void GetFloor()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "Floor")
            {
                floor = t.gameObject;
                return;
            }
        }
    }

    public void BuildFloorNavMesh()
    {
        floor.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void GetHallways()
    {
        hallways.Clear();

        //loops through all children and add the ones that have a hallway script to hallway array
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Hallway>() != null)
                hallways.Add(child.gameObject.GetComponent<Hallway>());
        }

        foreach (Hallway hallway in hallways)
        {
            //checks if there is a hallway on the top side
            if (hallway.transform.position.z > transform.position.z)
            {
                hallway.direction = 1;
                topHallway = true;
            }

            //checks if there is a hallway on the right side
            if (hallway.transform.position.x > transform.position.x)
            {
                hallway.direction = 2;
                rightHallway = true;
            }

            //checks if there is a hallway on the bottom side
            if (hallway.transform.position.z < transform.position.z)
            {
                hallway.direction = 3;
                bottomHallway = true;
            }

            //checks if there is a hallway on the left side
            if (hallway.transform.position.x < transform.position.x)
            {
                hallway.direction = 4;
                leftHallway = true;
            }
        }
    }

    public void SetRoomByType()
    {
        switch (roomType)
        {
            case RoomType.StartRoom:
                SetRoomColour(Color.green);
                break;
            case RoomType.BossRoom:
                SetRoomColour(Color.red);
                break;
            case RoomType.EnemyRoom:
                SetRoomColour(Color.grey);
                break;
            case RoomType.EmptyRoom:
                SetRoomColour(Color.white);
                break;
            case RoomType.TreasureRoom:
                SetRoomColour(Color.yellow);
                break;
            case RoomType.TrapRoom:
                SetRoomColour(Color.blue);
                break;
            default:
                break;
        }
    }
    
    void SetRoomColour(Color colour)
    {
        floor.GetComponent<MeshRenderer>().material.color = colour;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRoomTrigger = true;
            controller.map.SetActiveRoom(transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRoomTrigger = false;
        }
    }

    public void OpenDoors()
    {
        foreach (Hallway hallway in hallways)
        {
            hallway.OpenDoor();
        }
    }

    public void CloseDoors()
    {
        foreach (Hallway hallway in hallways)
        {
            hallway.CloseDoor();
        }
    }

    public void StartRoom()
    {
        switch (roomType)
        {
            case RoomType.StartRoom:
                break;
            case RoomType.BossRoom:
                controller.StartBossRoom(this); 
                break;
            case RoomType.EnemyRoom:
                controller.StartEnemyRoom(this);
                break;
            case RoomType.EmptyRoom:
                break;
            case RoomType.TreasureRoom:
                controller.StartTreasureRoom(this);
                break;
            case RoomType.TrapRoom:
                controller.StartTrapRoom(this);
                break;
            default:
                break;
        }
    }
}