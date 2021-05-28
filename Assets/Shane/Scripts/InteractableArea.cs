using System;
using UnityEngine;

//Holds info - what key to interact? Held down or not?
//Events for entering, leaving area.
public abstract class InteractableArea : MonoBehaviour, iInteractable
{
    //Events.
    public static event Action<InteractableArea> EnteredAreaEvent;
    public static event Action<InteractableArea> LeftAreaEvent;

    [Header("Interact Input")]
    [SerializeField] private bool HoldToInteract;
    public KeyCode keyToInteract;
    public Func<KeyCode,bool> inputDelegate; 
    
    //Start.
    private void Start()
    {
        if (HoldToInteract)
            inputDelegate = Input.GetKey;
        else
            inputDelegate = Input.GetKeyDown;
    }


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

    public abstract void Interact();
}
