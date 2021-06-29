using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List", menuName = "Items/New Item List")]
public class SOItemsList : ScriptableObject
{
    public List<BaseItem> allItems = new List<BaseItem>();
}
