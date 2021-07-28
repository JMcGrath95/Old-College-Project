using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaneFakeGameController : MonoBehaviour
{
    public static event Action LevelStartedEvent;
    public static event Action LevelEndedEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            LevelStartedEvent?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            LevelEndedEvent?.Invoke();
    }
}
