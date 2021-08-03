using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasController : MonoBehaviour
{
    public UI_InteractPopup interactPopup;

    private void Start()
    {
        InteractableArea.EnteredAreaEvent += InteractableArea_EnteredAreaEvent;
        InteractableArea.LeftAreaEvent += InteractableArea_LeftAreaEvent;
    }

    private void OnDestroy()
    {
        InteractableArea.EnteredAreaEvent -= InteractableArea_EnteredAreaEvent;
        InteractableArea.LeftAreaEvent -= InteractableArea_LeftAreaEvent;
    }

    private void InteractableArea_EnteredAreaEvent(InteractableArea obj)
    {
        interactPopup.Show();
    }

    private void InteractableArea_LeftAreaEvent(InteractableArea obj)
    {
        interactPopup.Hide();
    }
}
