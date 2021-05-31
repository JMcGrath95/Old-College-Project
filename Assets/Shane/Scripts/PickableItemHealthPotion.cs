using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItemHealthPotion : PickableItem
{
    public static event Action<string, int> PickedUpHealthPotionEvent;

    [SerializeField] private int healthToGive;

    public override void PlayerPickedMeUp()
    {
        print("picked up health");
        PickedUpHealthPotionEvent?.Invoke(itemID, healthToGive);
        Destroy(gameObject);
    }
}
