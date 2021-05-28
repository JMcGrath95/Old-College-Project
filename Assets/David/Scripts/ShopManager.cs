using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    
    public GameObject shopUI;
    [Header("Content Location")]
    public Transform Content;
    

    [Header("Shop Items")]
    public ItemList allItems;

    public void AddItem(string itemID, float amount)
    {


        foreach (var item in allItems.Items)
        {
            if (item.ItemID == itemID)
            {
                Instantiate(item.ItemBtnPrefab, Content);

                item.ItemBtnPrefab.GetComponent<ItembtnPrefab>().ItemImgIcon.sprite = item.itemImg;

                item.ItemBtnPrefab.GetComponent<ItembtnPrefab>().ItemDescription.text = item.Desciption;

                item.ItemBtnPrefab.GetComponent<ItembtnPrefab>().Currency.text = item.Price.ToString();
            }
        }
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
