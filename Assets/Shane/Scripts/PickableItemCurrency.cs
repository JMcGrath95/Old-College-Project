using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItemCurrency : PickableItem
{
    public static event Action<string, int> PickedUpCurrencyEvent;

    [SerializeField] private int currencyToGive;

    public override void PlayerPickedMeUp()
    {
        PickedUpCurrencyEvent?.Invoke(itemID,currencyToGive);
        Destroy(gameObject);
    }
}
