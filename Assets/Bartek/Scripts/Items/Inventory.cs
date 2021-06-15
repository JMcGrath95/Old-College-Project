using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemTest test;

    void Start()
    {
        test.test.Initiliaze(this.gameObject);
    }

    void Update()
    {
        test.test.TriggerEffect();
    }
}
