using System.Collections.Generic;
using UnityEngine;


//Putting currency here for now.

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ShaneItem> ItemList;
    [SerializeField] private int currency;

    private void Awake() { }
    private void Start() 
    {
        PickableItemCurrency.PlayerPickedUpCurrencyEvent += OnPlayerPickedUpCurrency;
        PickableInventoryItem.PlayerPickedUpInventoryEvent += OnPlayerPickedUpInventoryItem;
    }

    private void OnPlayerPickedUpInventoryItem(string itemID, PickableInventoryItem inventoryItem)
    {
        
    }

    private void OnPlayerPickedUpCurrency(string itemID,int currencyToAdd)
    {
        currency += currencyToAdd;
        print($"picked up currency. Currency is now {currency}");
    }

    private void AddItemToInventory(string itemID)
    {

    }

    private void OnDestroy()
    {
        PickableItemCurrency.PlayerPickedUpCurrencyEvent -= OnPlayerPickedUpCurrency;
        PickableInventoryItem.PlayerPickedUpInventoryEvent -= OnPlayerPickedUpInventoryItem;

    }
}
