using UnityEngine;

//Listens out for player pressing interact button.
//If it has a reference to an interactable area (i.e is inside one), it calls .Interact on it. 

public class PlayerInteraction : MonoBehaviour
{
    private iInteractable interactable;

    //Start.
    void Start()
    {
        InputManager.InteractInputEvent += OnInteractInput;
        InteractableArea.EnteredAreaEvent += OnEnteringInteractableArea;
        InteractableArea.LeftAreaEvent += OnLeavingInteractableArea;
    }

    private void OnLeavingInteractableArea(InteractableArea interactableArea) => interactable = null;
    private void OnEnteringInteractableArea(InteractableArea interactableArea) => interactable = interactableArea.GetComponent<iInteractable>();
    private void OnInteractInput()
    {
        if (interactable != null)
            interactable.Interact();
    }

    private void OnDestroy()
    {
        InputManager.InteractInputEvent -= OnInteractInput;
        InteractableArea.EnteredAreaEvent -= OnEnteringInteractableArea;
        InteractableArea.LeftAreaEvent -= OnLeavingInteractableArea;
    }
}
