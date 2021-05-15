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
    List<Room> createdRooms = new List<Room>();

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

                if (IsPositionFree(posToSpawn) && rooms.Count < maxRoomCount)
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
            RoomPositions.Add(position);

            AddRoom(room);
        }

        if (rooms.Count < maxRoomCount)
        {
            GenerateRooms();
        }
        else
        {
            InvokeRepeating("CreateRooms", 0.3f, 0.3f);
        }
    }

    int x = 0;
    void CreateRooms()
    {
        createdRooms.Add(Instantiate(rooms[x], RoomPositions[x], Quaternion.identity));
        x++;
        if (x >= rooms.Count)
        {
            CancelInvoke("CreateRooms");
            FixRoomsWithUnconnectedHallways();
        }
    }

    void FixRoomsWithUnconnectedHallways()
    {
        for (int i = 0; i < RoomPositions.Count; i++)
        {
            List<Room> t = new List<Room>();
            t.Clear();
            t.AddRange(allRooms);
            bool change = false;
            int hallwaysNeeded = 0;

            foreach (Hallway hallway in rooms[i].hallways)
            {
                Vector3 pos = GetPosition(hallway.direction, RoomPositions[i]);
                
                Debug.Log(rooms[i].name + " " + RoomPositions[i] + " " + hallway.direction);

                if (IsHallwayAtPosition(hallway.direction, pos) == false)
                {
                    change = true;
                    foreach (Room room in allRooms)
                    {
                        foreach (Hallway hall in room.hallways)
                        {
                            if (hall.direction == hallway.direction)
                            {
                                if (t.Contains(room))
                                {
                                    Debug.Log("room to remove : " + room.name);
                                    t.Remove(room);
                                }
                            }
                        }
                    }
                }
                else
                {
                    hallwaysNeeded++;
                }
            }

            foreach (Room room in allRooms)
            {
                if (rooms[i].topHallway == false && room.topHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                if (rooms[i].rightHallway == false && room.rightHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                if (rooms[i].bottomHallway == false && room.bottomHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                if (rooms[i].leftHallway == false && room.leftHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                if (room.hallways.Count != hallwaysNeeded)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }
            }



            if (change)
            {
                Destroy(createdRooms[i].gameObject);
                Instantiate(RandomizeRoom(t.ToArray()), RoomPositions[i], Quaternion.identity);
                /*print(createdRooms[i].name);
                foreach (Room o in t)
                {
                    print(o.name);
                }
                print(t.Count);
                Destroy(createdRooms[i].gameObject);
                createdRooms.RemoveAt(i);

                Room newRoom = Instantiate(RandomizeRoom(t.ToArray()), RoomPositions[i], Quaternion.identity);
                print(newRoom.name);
                createdRooms.Insert(i, newRoom);*/
            }

        }

        /*rooms.Clear();
        rooms.AddRange(createdRooms);
        print(rooms.Count);
        print(RoomPositions.Count);
        foreach (Room room1 in rooms)
        {
            print(room1.name);
        }*/
    }

    bool IsHallwayAtPosition(int dir, Vector3 pos)
    {
        if (IsPositionFree(pos))
            return false;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (pos == RoomPositions[i])
            {
                switch (dir)
                {
                    case 1:
                        if (rooms[i].bottomHallway)
                            return true;
                        break;
                    case 2:
                        if (rooms[i].leftHallway)
                            return true;
                        break;
                    case 3:
                        if (rooms[i].topHallway)
                            return true;
                        break;
                    case 4:
                        if (rooms[i].rightHallway)
                            return true;
                        break;
                }
            }
        }

        return false;
    }

    //checks adjacent positions and returns list of rooms that can be spawned in that position
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
        //checks based on hall bools what rooms can be spawned and assigns returnlist
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

        return returnList;
    }

    Room RandomizeRoom(Room[] roomsToChooseFrom)
    {
        //print(roomsToChooseFrom.Length);
        rand = Random.Range(0, roomsToChooseFrom.Length);
        return roomsToChooseFrom[rand];
    }

    void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        roomToAdd.GetHallways();
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