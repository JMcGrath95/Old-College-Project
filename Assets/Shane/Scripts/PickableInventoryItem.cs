using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableInventoryItem : PickableItem
{
    public static event Action<string,PickableInventoryItem> PlayerPickedUpInventoryEvent;

    public override void PlayerPickedMeUp()
    {
        PlayerPickedUpInventoryEvent?.Invoke(itemID,this);
        Destroy(gameObject);
    }
}
