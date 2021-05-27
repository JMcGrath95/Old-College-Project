using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemList", menuName = "Items")]
public class ItemList : ScriptableObject
{
    public List<ShopScriptData> Items;

    public ShopScriptData GetItem(string id)
    {
        return Items.Find(item => item.ItemID == id);
    }
}
