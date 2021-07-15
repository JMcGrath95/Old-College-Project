using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enum is used to define when the item is used e.g Attacking type items are used on player attack
public enum ItemUseType
{
    Attacking,
    InstantUse,
}

public abstract class BaseItem : ScriptableObject
{
    public string ItemName = "New Item";                        //display/search name
    public Sprite DisplayImage;                                 //image reference used for UI display
    public AudioClip ItemSound = null;                          //Audio for the item. some items can have no sound
    public ItemUseType ItemType = ItemUseType.InstantUse;       //This enum is used to define when the item is used e.g Attacking type items are used on player attack

    [HideInInspector]
    public GameObject Owner;                                    //reference to owner (set through InitializeEffect not inspector)

    public abstract void InitializeItem(GameObject owner);      //used when items gets added/used when any values needs to shared from item or to item
    public abstract void UseItem();                             //called when item is getting used to trigger what the item does
    
}
