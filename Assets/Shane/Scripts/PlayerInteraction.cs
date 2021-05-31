using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool IsInInteractionArea;

#if UNITY_STANDALONE
    private KeyCode keyCodeToInteract;
    private Func<KeyCode, bool> inputDelegate;

#elif UNITY_ANDROID || UNITY_IOS
    private Button btnInteract;
#endif

    private iInteractable interactable;

    //Start.
    void Start()
    {
        InteractableArea.EnteredAreaEvent += OnEnteredInteractableArea;
        InteractableArea.LeftAreaEvent += OnLeftInteractableArea;
    }

    private void OnEnteredInteractableArea(InteractableArea interactableArea)
    {
        IsInInteractionArea = true;
        interactable = interactableArea;

        UpdateInteractInfo(interactableArea);
    }

    private void OnLeftInteractableArea(InteractableArea interactableArea)
    {
        IsInInteractionArea = false;

        //If mobile.
#if UNITY_ANDROID || UNITY_IOS
        interactableArea.btnToInteract.onClick.RemoveListener(interactable.Interact);
#endif

    }


#if UNITY_STANDALONE
    private void Update()
    {
        //If PC.

        if (!IsInInteractionArea)
            return;

        if(inputDelegate(keyCodeToInteract))
        {
            interactable.Interact();
        }

    }
#endif


    private void UpdateInteractInfo(InteractableArea interactableArea)
    {

#if UNITY_STANDALONE

        inputDelegate = interactableArea.inputDelegate;
        keyCodeToInteract = interactableArea.keyToInteract;
        
#elif UNITY_ANDROID || UNITY_IOS

        interactableArea.btnToInteract.onClick.AddListener(interactable.Interact);
#endif
    }
    private void ResetInteractInfo(InteractableArea interactableArea)
    {
#if UNITY_STANDALONE

        inputDelegate = interactableArea.inputDelegate;
        keyCodeToInteract = interactableArea.keyToInteract;
        
#elif UNITY_ANDROID || UNITY_IOS

        interactableArea.btnToInteract.onClick.RemoveListener(interactable.Interact);
#endif
    }

    private void OnDestroy()
    {
        InteractableArea.EnteredAreaEvent -= OnEnteredInteractableArea;
        InteractableArea.LeftAreaEvent -= OnLeftInteractableArea;
    }
}
