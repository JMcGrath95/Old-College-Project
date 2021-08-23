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
        Player = GameObject.FindGameObjectWithTag("Player");

        foreach (Transform transform in Player.transform)
        {
            if (transform.gameObject.name == "PlayerModel")
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

        if(itemToAdd.ItemType == ItemUseType.InstantUse)
        {
            itemToAdd.InitializeItem(Player);
            itemToAdd.UseItem();
        }
        else
        {
            itemToAdd.InitializeItem(playerModel);
        }
    }

    private void OnDestroy()
    {
        PlayerAttack.PlayerAttackedEvent -= PlayerAttack_PlayerAttackedEvent;
    }
}