using UnityEngine;

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
