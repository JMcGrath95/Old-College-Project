using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Spawner spawnerPrefab;

    public void StartEnemyRoom(Room room)
    {
        room.CloseDoors();
        room.spawner = Instantiate(spawnerPrefab, room.transform);
        room.spawner.roomAssignedTo = room;
        room.spawner.SpawnEnemies();
    }

    public void StartBossRoom(Room room)
    {
        room.CloseDoors();
        room.spawner = Instantiate(spawnerPrefab, room.transform);
        room.spawner.roomAssignedTo = room;
        room.spawner.SpawnBossEnemy();
    }
}
