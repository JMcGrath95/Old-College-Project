using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PickableItem : MonoBehaviour
{
    public static event Action<string> PlayerPickedUpItemEvent;

    [Header("Item Info")]
    [SerializeField] protected string itemID;


    public abstract void PlayerPickedMeUp();

    private void OnTriggerEnter(Collider other)
    {
        //Change collision / layer settings to not have to check this?
        if (!other.CompareTag("Player"))
            return;

        PlayerPickedMeUp();

        PlayerPickedUpItemEvent?.Invoke(itemID);
    }
}
