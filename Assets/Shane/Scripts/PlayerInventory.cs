using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ShaneItem> ItemList;
    [SerializeField] private int currency;

    private void Awake() { }
    private void Start() 
    {
        PickableItemCurrency.PickedUpCurrencyEvent += OnPlayerPickedUpCurrency;
    }

    private void OnPlayerPickedUpCurrency(string itemID,int currencyToAdd)
    {
        currency += currencyToAdd;
        print($"picked up currency. Currency is now {currency}");
    }

    private void OnPlayerPickedUpItem(string itemID)
    {
        //Find item by ID. Add to list.
        //For now just creating one and adding.

        ItemList.Add(new ShaneItem(itemID, "Test Item", "Test Description"));
        print("added item");

    }



    private void AddItemToInventory(string itemID)
    {

    }


}
