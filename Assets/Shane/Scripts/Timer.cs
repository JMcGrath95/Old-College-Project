using System;
using System.Collections;
using UnityEngine;

public class Timer 
{
    public Timer(MonoBehaviour mono, float timeToStop, params Action[] callbacksOnTimerEnd)
    {
       mono.StartCoroutine(StartTimerCoroutine(timeToStop,callbacksOnTimerEnd));
    }

    private IEnumerator StartTimerCoroutine(float timeToStop,params Action[] callbacks)
    {
        float timer = 0f;

        while (timer <= timeToStop)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < callbacks.Length; i++)
        {
            callbacks[i]?.Invoke();
        }
    }
}
