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
        startRoom.positionToSpawnIn = Vector3.zero;
        AddRoom(startRoom);

        GenerateRooms();
    }

    List<Vector3> PositionsToPlaceRoomsIn = new List<Vector3>();
    int x = 0;
    void GenerateRooms()
    {
        PositionsToPlaceRoomsIn.Clear();

        foreach (Room room in rooms)
        {
            Vector3 pos = room.transform.position;

            foreach (Hallway hallway in room.hallways)
            {
                Vector3 posToSpawn = GetPosition(hallway.direction, room.transform.position);

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

            Room room = RandomizeRoom(r.ToArray());
            room.positionToSpawnIn = position;

            AddRoom(room);
        }

        foreach (Room room1 in rooms)
        {
            print(room1.name + " " + room1.positionToSpawnIn);
            Instantiate(room1, room1.positionToSpawnIn, Quaternion.identity);
        }
    }

    int i = 0;
    void CreateRooms()
    {
        //need tweaking
        if (rooms.Count < maxRoomCount)
        {
            GenerateRooms();
        }
        else if (i < rooms.Count)
        {
            Instantiate(rooms[i], rooms[i].positionToSpawnIn, Quaternion.identity);
            i++;
            Invoke("CreateRooms", 0.2f);
        }
    }

    List<Room> CheckAdjacentPositions(Vector3 posToCheckFrom)
    {
        List<Room> returnList = new List<Room>();

        //bool to check what hallways are required for new room
        bool topHall = false, rightHall = false, bottomHall = false, leftHall = false;

        //bool to check whether there is a wall in that direction
        bool topWall = false, rightWall = false, bottomWall = false, leftWall = false;

        //Adjacent Positions
        Vector3 topPos, rightPos, bottomPos, leftPos;

        //gets adjacent positions and assigns them 
        topPos = GetPosition(1, posToCheckFrom);
        rightPos = GetPosition(2, posToCheckFrom);
        bottomPos = GetPosition(3, posToCheckFrom);
        leftPos = GetPosition(4, posToCheckFrom);

        /*loops through all rooms and assigns bools based on whether 
        the room has a hallway in that position*/
        foreach (Room room in rooms)
        {
            //check if new room requires top hallway, if there is a wall in that direction or is the spot empty
            if (topPos == room.transform.position)
            {
                if (room.bottomHallway)
                {
                    topWall = false;
                    topHall = true;
                }
                else
                {
                    topHall = false;
                    topWall = true;
                }
            }

            //check if new room requires right hallway, if there is a wall in that direction or is the spot empty
            if (rightPos == room.transform.position)
            {
                if (room.leftHallway)
                {
                    rightWall = false;
                    rightHall = true;
                }
                else
                {
                    rightHall = false;
                    rightWall = true;
                }
            }

            //check if new room requires bottom hallway, if there is a wall in that direction or is the spot empty
            if (bottomPos == room.transform.position)
            {
                if (room.topHallway)
                {
                    bottomWall = false;
                    bottomHall = true;
                }
                else
                {
                    bottomHall = false;
                    bottomWall = true;
                }
            }

            //check if new room requires left hallway, if there is a wall in that direction or is the spot empty
            if (leftPos == room.transform.position)
            {
                if (room.rightHallway)
                {
                    leftWall = false;
                    leftHall = true;
                }
                else
                {
                    leftHall = false;
                    leftWall = true;
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
        return roomsToChooseFrom[rand];
    }

    void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        //print("Room Name : " + roomToAdd.name + ", Position : " + roomToAdd.transform.position + ", Hallways : " + roomToAdd.hallways.Count);
    }

    bool IsPositionFree(Vector3 position)
    {
        foreach (Room room in rooms)
        {
            if (room.positionToSpawnIn == position)
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