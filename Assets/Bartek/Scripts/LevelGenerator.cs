using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Room[] rooms_TRBL;
    public Room[] rooms_T;
    public Room[] rooms_R;
    public Room[] rooms_B;
    public Room[] rooms_L;

    Room startRoom;
    Room exitRoom;
    List<Room> potentialExitRooms = new List<Room>();
    List<Room> rooms = new List<Room>();

    Room currentRoom; 

    int roomsUp, roomsRight, roomsDown, roomsLeft;

    int rand;

    private void Start()
    {
        GenerateStartRoom();

        Invoke("Generate", 1);
    }

    void GenerateStartRoom()
    {
        rand = Random.Range(0, rooms_TRBL.Length);

        startRoom = Instantiate(rooms_TRBL[rand], Vector3.zero, Quaternion.identity);
        rooms.Add(startRoom);

        /*roomsRight = Random.Range(5, 11);
        roomsDown = Random.Range(5, 11);
        roomsLeft = Random.Range(5, 11);*/
    }

    void GenerateTopRooms()
    {
        roomsUp = Random.Range(5, 11);

        currentRoom = startRoom;

        GenerateRooms(roomsUp, 1);

    }

    bool IsPositionFree(Vector3 position)
    {
        foreach (Room room in rooms)
        {
            if (room.transform.position == position)
                return false;
        }

        return true;
    }

    void GenerateRooms(int points, int startingdirection)
    {
        Vector3 pos = GetPosition(startingdirection);
        //currentRoom = Instantiate()
    }

    void Generate()
    {
        currentRoom = startRoom;
        print(currentRoom.hallways.Count);
        foreach (Hallway hallway in currentRoom.hallways)
        {
            Vector3 pos = GetPosition(hallway.direction);

            if(IsPositionFree(pos))
            {
                GenerateRoom(hallway.direction, pos);
            }
        }
    }

    Vector3 GetPosition(int direction)
    {
        Vector3 pos = currentRoom.transform.position;

        Vector3 returnPos = new Vector3();

        switch(direction)
        {
            //Gets room spawn postion if direction is up
            case 1:
                returnPos =  new Vector3(pos.x, pos.y, pos.z + 30);
                break;
            //Gets room spawn postion if direction is right
            case 2:
                returnPos = new Vector3(pos.x + 30, pos.y, pos.z);
                break;
            //Gets room spawn postion if direction is down
            case 3:
                returnPos = new Vector3(pos.x, pos.y, pos.z - 30);
                break;
            //Gets room spawn postion if direction is left
            case 4:
                returnPos = new Vector3(pos.x - 30, pos.y, pos.z);
                break;
        }

        return returnPos;
    }

    void GenerateRoom(int direction, Vector3 pos)
    {
        switch (direction)
        {
            case 1:
                rand = Random.Range(0, rooms_B.Length);
                rooms.Add(Instantiate(rooms_B[rand], pos, Quaternion.identity));
                break;
            case 2:
                rand = Random.Range(0, rooms_L.Length);
                rooms.Add(Instantiate(rooms_L[rand], pos, Quaternion.identity));
                break;
            case 3:
                rand = Random.Range(0, rooms_T.Length);
                rooms.Add(Instantiate(rooms_T[rand], pos, Quaternion.identity));
                break;
            case 4:
                rand = Random.Range(0, rooms_R.Length);
                rooms.Add(Instantiate(rooms_R[rand], pos, Quaternion.identity));
                break;
        }
    }
}
