using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ShaneItem> ItemList;

    private void Awake() { }
    private void Start()
    {
        PickableItem.PlayerPickedUpItemEvent += OnPlayerPickedUpItem;
    }

    private void OnPlayerPickedUpItem(string itemID)
    {
        //Find item by ID. Add to list.
        //For now just creating one and adding.

        ItemList.Add(new ShaneItem(itemID, "Test Item", "Test Description"));

    }

    private void AddItemToInventory(string itemID)
    {

    }


}
