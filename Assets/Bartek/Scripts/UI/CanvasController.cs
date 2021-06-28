using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasController : MonoBehaviour
{
    public GameObject InteractPopup;

    private void Start()
    {
        DontDestroyOnLoad(this);

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
        SetInteractPopup(true);
    }

    private void InteractableArea_LeftAreaEvent(InteractableArea obj)
    {
        SetInteractPopup(false);
    }

    public void SetInteractPopup(bool value)
    {
        InteractPopup.SetActive(value);
    }
}
