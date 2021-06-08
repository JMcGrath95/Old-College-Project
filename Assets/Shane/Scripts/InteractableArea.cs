using System;
using UnityEngine;
using UnityEngine.UI;


public abstract class InteractableArea : MonoBehaviour, iInteractable
{
    //Events.
    public static event Action<InteractableArea> EnteredAreaEvent;
    public static event Action<InteractableArea> LeftAreaEvent;
    public abstract void Interact();


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        EnteredAreaEvent?.Invoke(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        LeftAreaEvent?.Invoke(this);
    }

}
