using System;
using UnityEngine;

//Holds info - what key to interact? Held down or not?
//Events for entering, leaving area.
public abstract class InteractableArea : MonoBehaviour, iInteractable
{
    //Events.
    public static event Action<InteractableArea> EnteredAreaEvent;
    public static event Action<InteractableArea> LeftAreaEvent;

    //PC Interaction.
#if UNITY_STANDALONE
    [Header("Interact Input")]
    [SerializeField] private bool HoldToInteract;
    public KeyCode keyToInteract;
    public Func<KeyCode,bool> inputDelegate;

#elif UNITY_ANDROID || UNITY_IOS
    public Button btnToInteract;
#endif


    //Start.
    public virtual void Start()
    {
#if UNITY_STANDALONE

        if (HoldToInteract)
            inputDelegate = Input.GetKey;
        else
            inputDelegate = Input.GetKeyDown;


#elif UNITY_ANDROID || UNITY_IOS



#endif
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
