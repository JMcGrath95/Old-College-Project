using System;
using UnityEngine;

public class PickableItemHealthPotion : PickableItem
{
    public static event Action<string, int> PickedUpHealthPotionEvent;

    [SerializeField] private int healthToGive;

    public override void PlayerPickedMeUp()
    {
        PickedUpHealthPotionEvent?.Invoke(itemID, healthToGive);
        Destroy(gameObject);
    }
}
