using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public List<BaseItem> allItemList = new List<BaseItem>();
    List<BaseItem> lootPool = new List<BaseItem>();

    public PickupItem itemPrefab;

    private void Start()
    {
        lootPool = allItemList;
    }

    public BaseItem ReturnRandomItem()
    {
        if (lootPool.Count < 1)
        {
            return (allItemList[0]);
        }

        BaseItem item = lootPool[Random.Range(0, lootPool.Count)];
        lootPool.Remove(item);

        return item;
    }

    public void SpawnItemPrefab(Room room)
    {
        Instantiate(itemPrefab, room.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
    }
}