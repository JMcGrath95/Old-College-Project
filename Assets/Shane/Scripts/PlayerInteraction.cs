using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool IsInInteractionArea;

    private KeyCode keyCodeToInteract;
    private Func<KeyCode, bool> inputDelegate;

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
        UpdateInteractInfo(interactableArea);
    }

    private void OnLeftInteractableArea(InteractableArea interactableArea)
    {
        IsInInteractionArea = false;
    }

    private void Update()
    {
        if (!IsInInteractionArea)
            return;

        if(inputDelegate(keyCodeToInteract))
        {
            interactable.Interact();
        }

    }

    private void UpdateInteractInfo(InteractableArea interactableArea)
    {
        inputDelegate = interactableArea.inputDelegate;
        keyCodeToInteract = interactableArea.keyToInteract;
        interactable = interactableArea;
    }

    private void OnDestroy()
    {
        InteractableArea.EnteredAreaEvent -= OnEnteredInteractableArea;
        InteractableArea.LeftAreaEvent -= OnLeftInteractableArea;
    }
}
