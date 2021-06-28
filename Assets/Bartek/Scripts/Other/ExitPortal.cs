using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : InteractableArea
{
    public override void Interact()
    {
        Application.Quit();
    }

}
