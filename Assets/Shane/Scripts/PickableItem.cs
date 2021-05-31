using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickableItem : MonoBehaviour
{
    public static event Action<string> PlayerPickedUpItemEvent;

    [Header("Item Info")]
    [SerializeField] private string itemID;


    private void OnTriggerEnter(Collider other)
    {
        //Change collision / layer settings to not have to check this?
        if (!other.CompareTag("Player"))
            return;

        PlayerPickedUpItemEvent?.Invoke(itemID);
    }
}
