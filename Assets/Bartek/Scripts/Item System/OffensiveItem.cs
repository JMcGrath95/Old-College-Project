using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveItem : BaseItem
{
    public override void InitializeItem(GameObject owner)
    {
        Debug.Log(ItemName + " item initiliazed.");
    }

    public override void UseItem()
    {
        Debug.Log(ItemName + " item used.");
    }
}
