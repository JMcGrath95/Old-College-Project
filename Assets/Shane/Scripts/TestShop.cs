using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShop : InteractableArea
{
    public override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        print("Interacted with shop");
    }
}
