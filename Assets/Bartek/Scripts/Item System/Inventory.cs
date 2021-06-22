using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ProjectileItem> ProjectileItems = new List<ProjectileItem>();
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

        //add code tying offensive items to attack event of player
    }

    void Update()
    {

    }
}
