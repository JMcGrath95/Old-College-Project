using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Spawner spawnerPrefab;
    public ExitPortal portalPrefab;

    public ItemController itemController;

    public GameObject trapPrefab;

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

    public void StartTreasureRoom(Room room)
    {
        itemController.SpawnItemPrefab(room);
        room.roomCleared = true;
    }

    public void StartTrapRoom(Room room)
    {
        itemController.SpawnItemPrefab(room);
        Instantiate(trapPrefab, room.transform);
        room.roomCleared = true;
    }
}
