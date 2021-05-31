using System;
using UnityEngine;

public class PickableItemCurrency : PickableItem
{
    public static event Action<string, int> PlayerPickedUpCurrencyEvent;

    [SerializeField] private int currencyToGive;

    public override void PlayerPickedMeUp()
    {
        PlayerPickedUpCurrencyEvent?.Invoke(itemID,currencyToGive);
        Destroy(gameObject);
    }
}
