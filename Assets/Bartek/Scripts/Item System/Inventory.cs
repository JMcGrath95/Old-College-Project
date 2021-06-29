using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> items = new List<BaseItem>();
    
    public GameObject Player;

    public float Money;

    void Start()
    {
        foreach (BaseItem item in items)
        {
            item.InitializeItem(Player);
        }
        
        foreach (BaseItem item in items)
        {
            item.UseItem();
        }

    }
}
