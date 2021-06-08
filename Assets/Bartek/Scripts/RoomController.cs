using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Spawner spawnerPrefab;
    public ExitPortal portalPrefab;
    public List<Item> items = new List<Item>();

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
        Instantiate(items[Random.Range(0, items.Count)], room.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        room.roomCleared = true;
    }
}
