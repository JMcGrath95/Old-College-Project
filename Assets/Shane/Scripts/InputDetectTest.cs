using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputDetectTest : MonoBehaviour
{
    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>().ToArray();

    void Start()
    {
        
    }

    
    void Update()
    {
        if (!Input.anyKey)
            return;


        print(GetCurrentKeyDown());

    }

    private static KeyCode? GetCurrentKeyDown()
    {
        if (!Input.anyKey)
        {
            return null;
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                //print(keyCodes[i]);
                return keyCodes[i];
            }
        }
        return null;
    }



}
