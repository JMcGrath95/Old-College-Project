using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    
    public  GameObject shopUI;

   

    public void BuyItem(int selectedItem)
    {

    }

    public void OpenShopMenu()
    {
        shopUI.SetActive(true);
    }

    public void CloseShopMenu()
    {
        shopUI.SetActive(false);
    }
}
