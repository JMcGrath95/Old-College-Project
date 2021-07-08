using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> items = new List<BaseItem>();

    GameObject Player;
    GameObject playerModel;

    public float Money;

    void Start() 
    {
        DontDestroyOnLoad(this);

        Player = GameObject.FindGameObjectWithTag("Player");

        foreach (Transform transform in Player.transform)
        {
            if(transform.gameObject.name == "Player Model")
            {
                playerModel = transform.gameObject;
                continue;
            }
        }

        PlayerAttack.PlayerAttackedEvent += PlayerAttack_PlayerAttackedEvent;

        foreach (BaseItem item in items)
        {
            item.InitializeItem(playerModel);
        }
    }

    private void PlayerAttack_PlayerAttackedEvent()
    {
        foreach (BaseItem item in items)
        {
            if(item.ItemType == ItemUseType.Attacking)
            {
                item.UseItem();
            }
        }
    }

    public void AddItem(BaseItem itemToAdd)
    {
        items.Add(itemToAdd);
        itemToAdd.InitializeItem(playerModel);

        if(itemToAdd.ItemType == ItemUseType.InstantUse)
        {
            itemToAdd.UseItem();
        }
    }


}