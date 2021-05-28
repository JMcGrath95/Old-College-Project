using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    ShopManager manager;
    public static shop Inatance;
    public ItemList item;
    bool InRange;
    

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

    private void Update()
    {
        if(InRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Opened");
                manager.OpenShopMenu();
            }
        }
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            InRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        manager.CloseShopMenu();
        InRange = false;
    }
}
