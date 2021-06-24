using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ProjectileItem> ProjectileItems = new List<ProjectileItem>();
    public List<ValueChangingItem> ValueChangingItems = new List<ValueChangingItem>();
    public GameObject Player;

    void Start()
    {
        foreach (ProjectileItem projectileItem in ProjectileItems)
        {
            projectileItem.InitializeItem(Player);
        }
        
        foreach (ProjectileItem projectileItem in ProjectileItems)
        {
            projectileItem.UseItem();
        }
        
        
        foreach (ValueChangingItem ValueChangingItem in ValueChangingItems)
        {
            ValueChangingItem.InitializeItem(Player);
        }

        foreach (ValueChangingItem ValueChangingItem in ValueChangingItems)
        {
            ValueChangingItem.UseItem();
        }

        //add code tying offensive items to attack event of player
    }

    void Update()
    {

    }
}
