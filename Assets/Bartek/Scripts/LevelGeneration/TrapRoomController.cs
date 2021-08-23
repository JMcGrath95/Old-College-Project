using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomController : MonoBehaviour
{
    public Bullet_Controller trapRoomSentryPrefab;
    Bullet_Controller trapRoomSentry;

    ItemController iController;
    Room room;

    public void InitializeController(ItemController ic, Room room)
    {
        iController = ic;
        this.room = room;
    }

    public void StartController()
    {
        trapRoomSentry = Instantiate(trapRoomSentryPrefab, room.transform.position + new Vector3(0,2.5f,0), Quaternion.identity);
        trapRoomSentry.GetComponent<BaseHealth>().DeathEvent += TrapRoomController_DeathEvent;
    }

    private void TrapRoomController_DeathEvent()
    {
        room.roomCleared = true;
        room.OpenDoors();
        iController.SpawnItemPrefab(room);
        Destroy(gameObject);
    }
}
