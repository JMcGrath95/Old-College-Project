using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameController gameController;

    //prefab rooms to choose from in the generator
    public Room[] rooms_TRBL;
    public Room[] rooms_T;
    public Room[] rooms_R;
    public Room[] rooms_B;
    public Room[] rooms_L;
    public Room[] allRooms;

    //variables for the first and last room for player spawn and boss spawn
    Room startRoom;
    Room exitRoom;
    Room treasureRoom;

    //lsit of rooms use in the generator
    List<Room> rooms = new List<Room>();

    //list of final rooms created by the generator
    List<Room> createdRooms = new List<Room>();

    //value for randomisation and a value for limit of rooms to have
    int rand, roomLimit;

    //values to randomise betwenn to set roomLimit
    public int minRoomCount, maxRoomCount;

    //list of all the room positions
    List<Vector3> RoomPositions = new List<Vector3>();

    //calls starting method for generator
    private void Start()
    {
        GenerateStartingRoom();
    }

    void GenerateStartingRoom()
    {
        //maxRoomCount can be between 15-20 rooms minus start room
        roomLimit = Random.Range(minRoomCount, maxRoomCount + 1);

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

        //chooses first room and assigns it to startroom and lists
        Room startRoom;
        startRoom = RandomizeRoom(r.ToArray());
        RoomPositions.Add(Vector3.zero);
        AddRoom(startRoom);

        //starts the generator
        GenerateRooms();
    }

    //list to use for storing free positions for a room to be placed in. only used in method below
    List<Vector3> PositionsToPlaceRoomsIn = new List<Vector3>();
    void GenerateRooms()
    {
        //clears list each cycle to prevent rooms being made in a position that is already taken
        PositionsToPlaceRoomsIn.Clear();

        //loops and checks that decide whether a position is free to be used for a room 
        //if it is assigns it to PositionsToPlaceRoomsIn list
        for (int i = 0; i < rooms.Count; i++)
        {
            Vector3 pos = RoomPositions[i];

            foreach (Hallway hallway in rooms[i].hallways)
            {
                Vector3 posToSpawn = GetPosition(hallway.direction, pos);

                if (IsPositionFree(posToSpawn) && rooms.Count < roomLimit)
                {
                    PositionsToPlaceRoomsIn.Add(posToSpawn);
                }
            }
        }

        //checks adjacent positions for each position in PositionsToPlaceRoomsIn list
        //checks what rooms can be spawned in those adjacent positions and assings them to r
        //then randomises a room in that list and add it to the rooms list
        foreach (Vector3 position in PositionsToPlaceRoomsIn)
        {
            List<Room> r = CheckAdjacentPositions(position);

            Room room = RandomizeRoom(r.ToArray());
            RoomPositions.Add(position);

            AddRoom(room);
        }

        //if the room limit is not reached repeat the method
        if (rooms.Count < roomLimit)
        {
            GenerateRooms();
        }
        //if the room limit is reached invoke create rooms to keep creating rooms every 0.3seconds
        else
        {
            InvokeRepeating("CreateRooms", 0.3f, 0.3f);
        }
    }

    int x = 0;
    void CreateRooms()
    {
        //creates the room object in the game and assigns it to createdrooms list
        createdRooms.Add(Instantiate(rooms[x], RoomPositions[x], Quaternion.identity));

        //gets the floor of the room and assigns it to a value in room script
        createdRooms[x].GetFloor();

        //increments count
        x++;

        //if max rooms is hit stop invoke of this method and call to fix broken hallways
        if (x >= rooms.Count)
        {
            CancelInvoke("CreateRooms");
            FixRoomsWithUnconnectedHallways();
        }
    }

    //method loops through all the rooms and checks if the hallways are pointing to emptiness or not
    //if they are remove those options from a list of rooms until a room that suits the position can be used
    void FixRoomsWithUnconnectedHallways()
    {
        for (int i = 0; i < RoomPositions.Count; i++)
        {
            //each loop resets values tso next loop wont have extra/broken values

            //list of rooms to chop down for what room to change the room into
            List<Room> t = new List<Room>();
            t.Clear();
            t.AddRange(allRooms);

            //bool for whether or not this rooms needs to change/has a hallway pointed to emptiness
            bool change = false;

            //int for how many hallways the new room will need
            int hallwaysNeeded = 0;

            //loops through all the hallways in the room being checked
            foreach (Hallway hallway in rooms[i].hallways)
            {
                //gets the positions the hallway is facing 
                Vector3 pos = GetPosition(hallway.direction, RoomPositions[i]);

                Debug.Log(rooms[i].name + " " + RoomPositions[i] + " " + hallway.direction);

                //checks if there is a hallway at that position facing this hallway
                //if false the room needs to be changed so it doesnt have this hallway
                //so it will cut down t list that has any hallway with a matching direction
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
                //if the previous statement is true that means that this room
                //needs this hallway and adds 1 to the hallways needed counter
                else
                {
                    hallwaysNeeded++;
                }
            }
            
            //loops through all the room presets
            //checks room being checked against them and removes any that have a hallway that this room doesn't
            foreach (Room room in allRooms)
            {
                //if room being checked doesn't have a hallway going up and this room does removes it
                if (rooms[i].topHallway == false && room.topHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                //if room being checked doesn't have a hallway going right and this room does removes it
                if (rooms[i].rightHallway == false && room.rightHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                //if room being checked doesn't have a hallway going down and this room does removes it
                if (rooms[i].bottomHallway == false && room.bottomHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                //if room being checked doesn't have a hallway going left and this room does removes it
                if (rooms[i].leftHallway == false && room.leftHallway == true)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }

                //if this  room has a number of hallways that doesn't match the hallwaysNeeded counter remove it from the list 
                if (room.hallways.Count != hallwaysNeeded)
                {
                    if (t.Contains(room))
                    {
                        Debug.Log("room to remove : " + room.name);
                        t.Remove(room);
                    }
                }
            }

            //if this room has to be changed
            if (change)
            {
                //destroy the room and remove it from the list
                Destroy(createdRooms[i].gameObject);
                createdRooms.RemoveAt(i);

                //create the new room and run its methods
                Room newRoom = Instantiate(RandomizeRoom(t.ToArray()), RoomPositions[i], Quaternion.identity);
                newRoom.GetFloor();
                newRoom.GetHallways();

                //insert new room into the postion of the list where the old room was
                print(newRoom.name + " " + newRoom.transform.position);
                createdRooms.Insert(i, newRoom);
            }
        }

        //clears old room list and assigns created rooms to it
        rooms.Clear();
        rooms.AddRange(createdRooms);

        DefineRoomTypes();

        gameController.StartGame();
    }

    void DefineRoomTypes()
    {
        for (int i = 0; i < createdRooms.Count; i++)
        {
            int random;
            
            //setting first room as startRoom
            if (i == 0)
            {
                createdRooms[i].roomType = RoomType.StartRoom;
                startRoom = createdRooms[i];
            }
            //setting last room as BossRoom
            else if (i == createdRooms.Count - 1)
            {
                createdRooms[i].roomType = RoomType.BossRoom;
                exitRoom = createdRooms[i];
            }
            else
            {
                //setting remaining rooms as empty or enemy rooms
                random = Random.Range(0, 5);
                if (random < 2)
                {
                    createdRooms[i].roomType = RoomType.EmptyRoom;
                }
                else
                {
                    createdRooms[i].roomType = RoomType.EnemyRoom;
                }
            }
        }

        //setting a room with one hallway that is not the BossRoom as TreasureRoom
        List<Room> potentialTreasureRooms = new List<Room>();
        foreach (Room r in createdRooms)
        {
            if (r.hallways.Count < 2 && r.transform.position != exitRoom.transform.position)
            {
                potentialTreasureRooms.Add(r);
            }
        }

        Room tr = RandomizeRoom(potentialTreasureRooms.ToArray());
        tr.roomType = RoomType.TreasureRoom;
        treasureRoom = tr;

        foreach (Room cr in createdRooms)
        {
            cr.SetRoomByType();
        }
    }

    //checks if there is a hallway facing the position passed in and opposite the direction passed in
    bool IsHallwayAtPosition(int dir, Vector3 pos)
    {
        //first checks if there is a room at that position otherwise returns false
        if (IsPositionFree(pos))
            return false;

        //loops through all rooms and their hallways to check id there is a hallway with the direction opposite to the one passed in
        //this sees if there is a hallway connected to another hallway or if it is going into emptiness
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

    //put in list of rooms and it returns a random room from the list passed in
    Room RandomizeRoom(Room[] roomsToChooseFrom)
    {
        //print(roomsToChooseFrom.Length);
        rand = Random.Range(0, roomsToChooseFrom.Length);
        return roomsToChooseFrom[rand];
    }

    //adds room to the lsit and runs its methods
    void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        roomToAdd.GetHallways();
    }

    //checks if position passed in is not already occupied and not looking to be occupied when method GenerateRooms is running
    bool IsPositionFree(Vector3 position)
    {
        //returns false if position passed doesn't exist in RoomPositions list
        for (int i = 0; i < RoomPositions.Count; i++)
        {
            if (RoomPositions[i] == position)
                return false;
        }

        //returns false if position passed doesn't exist in PositionsToPlaceRoomsIn list 
        foreach (Vector3 item in PositionsToPlaceRoomsIn)
        {
            if (item == position)
                return false;
        }

        //if both checks fail that means position is free and returns true for that position
        return true;
    }

    //Gets the position based on position of room and direction of hallway
    //if room is at (0,0,0) and hallway is to the right returns (x = 30, y = 0, z = 0)
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