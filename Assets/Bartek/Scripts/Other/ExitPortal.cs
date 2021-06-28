using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : InteractableArea
{
    public GameObject WinScreen;
    Canvas canvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();        
    }

    public override void Interact()
    {
        Instantiate(WinScreen, canvas.transform);
        Destroy(gameObject);
    }

}
