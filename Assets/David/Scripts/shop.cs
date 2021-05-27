using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    ShopManager manager;

    //[Header("Shop Items")]
    //public ItemList allItems;

    

    void Start()
    {
        Canvas Canvas = FindObjectOfType<Canvas>();

        foreach (Transform T  in Canvas.transform)
        {
            if (T.gameObject.name == "ShopManager")
            {
                manager = T.gameObject.GetComponent<ShopManager>();
                return;
            }
        }

        
    }


    


    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Opened");
                manager.OpenShopMenu();

                manager.AddItem("Health", 500);
                manager.AddItem("Poison", 200);
                //AddItem("Health", 500);
            }
        }
        Debug.Log("Working");
    }

    private void OnTriggerExit(Collider other)
    {
        manager.CloseShopMenu();
    }
}
