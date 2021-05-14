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
    public Room[] allRooms;

    Room startRoom;
    Room exitRoom;
    List<Room> potentialExitRooms = new List<Room>();
    List<Room> rooms = new List<Room>();

    Room currentRoom;

    int rand, maxRoomCount;

    int roomsUp, roomsRight, roomsDown, roomsLeft;

    List<Vector3> RoomPositions = new List<Vector3>();

    private void Start()
    {
        GenerateStartingRoom();
    }

    void GenerateStartingRoom()
    {
        //maxRoomCount can be between 15-20 rooms minus start room
        maxRoomCount = Random.Range(15, 21);

        //list of rooms with more then one hallway so start has more options
        List<Room> r = new List<Room>();
        foreach (Room room in allRooms)
        {
            room.GetHallways();

            if (room.hallways.Count >= 2)
            {
                r.Add(room);
            }
        }

        Room startRoom;
        startRoom = RandomizeRoom(r.ToArray());
        RoomPositions.Add(Vector3.zero);
        AddRoom(startRoom);

        GenerateRooms();
    }

    List<Vector3> PositionsToPlaceRoomsIn = new List<Vector3>();


    void GenerateRooms()
    {
        PositionsToPlaceRoomsIn.Clear();

        for (int i = 0; i < rooms.Count; i++)
        {
            Vector3 pos = RoomPositions[i];

            foreach (Hallway hallway in rooms[i].hallways)
            {
                Vector3 posToSpawn = GetPosition(hallway.direction, pos);

                if (IsPositionFree(posToSpawn))
                {
                    PositionsToPlaceRoomsIn.Add(posToSpawn);
                }
            }
        }

        foreach (Vector3 position in PositionsToPlaceRoomsIn)
        {
            List<Room> r = new List<Room>();
            r = CheckAdjacentPositions(position);
            foreach (Room room1 in r)
            {
                print(room1.name);
            }
            Room room = RandomizeRoom(r.ToArray());
            RoomPositions.Add(position);

            AddRoom(room);
        }

        if (rooms.Count < maxRoomCount)
        {
            GenerateRooms();
        }
        else
        {
            InvokeRepeating("Test", 0.3f, 0.3f);
        }
    }

    int x = 0;
    void Test()
    {
        Instantiate(rooms[x], RoomPositions[x], Quaternion.identity);
        x++;
        if (x >= rooms.Count)
        {
            CancelInvoke("Test");
        }
    }

    int p = 0;
    void CreateRooms()
    {
        //need tweaking
        if (rooms.Count < maxRoomCount)
        {
            GenerateRooms();
        }
        else if (p < rooms.Count)
        {
            Instantiate(rooms[p], RoomPositions[p], Quaternion.identity);
            p++;
            Invoke("CreateRooms", 0.2f);
        }
    }

    List<Room> CheckAdjacentPositions(Vector3 posToCheckFrom)
    {
        List<Room> returnList = new List<Room>();

        returnList.Clear();

        //bool to check what hallways are required for new room
        bool topHall = false, rightHall = false, bottomHall = false, leftHall = false;

        //Adjacent Positions
        Vector3 topPos, rightPos, bottomPos, leftPos;

        //gets adjacent positions and assigns them 
        topPos = GetPosition(1, posToCheckFrom);
        rightPos = GetPosition(2, posToCheckFrom);
        bottomPos = GetPosition(3, posToCheckFrom);
        leftPos = GetPosition(4, posToCheckFrom);

        /*loops through all rooms and assigns bools based on whether 
        the room has a hallway in that position*/
        for (int i = 0; i < rooms.Count; i++)
        {
            //check if new room requires top hallway, if there is a wall in that direction or is the spot empty
            if (topPos == RoomPositions[i])
            {
                if (rooms[i].bottomHallway)
                {
                    topHall = true;
                }
                else
                {
                    topHall = false;
                }
            }

            //check if new room requires right hallway, if there is a wall in that direction or is the spot empty
            if (rightPos == RoomPositions[i])
            {

                if (rooms[i].leftHallway)
                {
                    rightHall = true;
                }
                else
                {
                    rightHall = false;
                }
            }

            //check if new room requires bottom hallway, if there is a wall in that direction or is the spot empty
            if (bottomPos == RoomPositions[i])
            {

                if (rooms[i].topHallway)
                {
                    bottomHall = true;
                }
                else
                {
                    bottomHall = false;
                }
            }

            //check if new room requires left hallway, if there is a wall in that direction or is the spot empty
            if (leftPos == RoomPositions[i])
            {

                if (rooms[i].rightHallway)
                {
                    leftHall = true;
                }
                else
                {
                    leftHall = false;
                }
            }
        }

        //gotta change to take walls into account
        //checks based on bools what rooms can be spawned and assigns returnlist
        if (topHall)
        {
            if (rightHall)
            {
                if (bottomHall)
                {
                    //all hallways required
                    if (leftHall)
                    {
                        returnList.AddRange(rooms_TRBL);
                    }
                    //top, right and bottom hallway required
                    else
                    {
                        foreach (Room room in rooms)
                        {
                            if (room.topHallway && room.rightHallway && room.bottomHallway)
                                returnList.Add(room);
                        }
                    }
                }
                //top, right and left hallway required
                else if (leftHall)
                {
                    foreach (Room room in rooms)
                    {
                        if (room.topHallway && room.rightHallway && room.leftHallway)
                            returnList.Add(room);
                    }
                }
                //top and right hallway required
                else
                {
                    foreach (Room room in rooms)
                    {
                        if (room.topHallway && room.rightHallway)
                            returnList.Add(room);
                    }
                }
            }
            else if (bottomHall)
            {
                //top, bottom and left hallway required
                if (leftHall)
                {
                    foreach (Room room in rooms)
                    {
                        if (room.topHallway && room.bottomHallway && room.leftHallway)
                            returnList.Add(room);
                    }
                }
                //topand bottom hallway required
                else
                {
                    foreach (Room room in rooms)
                    {
                        if (room.topHallway && room.bottomHallway)
                            returnList.Add(room);
                    }
                }
            }
            //top and left hallway required
            else if (leftHall)
            {
                foreach (Room room in rooms)
                {
                    if (room.topHallway && room.leftHallway)
                        returnList.Add(room);
                }
            }
            //only top hallway required
            else
            {
                returnList.AddRange(rooms_T);
            }
        }
        else if (rightHall)
        {
            if (bottomHall)
            {
                //right, bottom and left hallway required
                if (leftHall)
                {
                    foreach (Room room in rooms)
                    {
                        if (room.rightHallway && room.bottomHallway && room.leftHallway)
                            returnList.Add(room);
                    }
                }
                //right and bottom hallway required
                else
                {
                    foreach (Room room in rooms)
                    {
                        if (room.rightHallway && room.bottomHallway)
                            returnList.Add(room);
                    }
                }
            }
            //right and left hallway required
            else if (leftHall)
            {
                foreach (Room room in rooms)
                {
                    if (room.rightHallway && room.leftHallway)
                        returnList.Add(room);
                }
            }
            //only right hallway required
            else
            {
                returnList.AddRange(rooms_R);
            }
        }
        else if (bottomHall)
        {
            //bottom and left hallway required
            if (leftHall)
            {
                foreach (Room room in rooms)
                {
                    if (room.leftHallway && room.bottomHallway)
                        returnList.Add(room);
                }
            }
            //only bottom hallway required
            else
            {
                returnList.AddRange(rooms_B);
            }
        }
        else if (leftHall)
        {
            //only left hallway required
            returnList.AddRange(rooms_L);
        }
        else
        {
            //no hallway required
            returnList.AddRange(rooms_TRBL);
        }

        //print(returnList.Count);

        return returnList;
    }

    Room RandomizeRoom(Room[] roomsToChooseFrom)
    {
        rand = Random.Range(0, roomsToChooseFrom.Length);
        print(rand);
        print(roomsToChooseFrom.Length);
        return roomsToChooseFrom[rand];
    }

    void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        roomToAdd.GetHallways();
        //print("Room Name : " + roomToAdd.name + ", Position : " + roomToAdd.transform.position + ", Hallways : " + roomToAdd.hallways.Count);
    }

    bool IsPositionFree(Vector3 position)
    {
        for (int i = 0; i < RoomPositions.Count; i++)
        {
            if (RoomPositions[i] == position)
                return false;
        }

        foreach (Vector3 item in PositionsToPlaceRoomsIn)
        {
            if (item == position)
                return false;
        }

        return true;
    }

    Vector3 GetPosition(int direction, Vector3 pos)
    {
        Vector3 returnPos = new Vector3();

        switch (direction)
        {
            //Gets room spawn postion if direction is up
            case 1:
                returnPos = new Vector3(pos.x, pos.y, pos.z + 30);
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
}