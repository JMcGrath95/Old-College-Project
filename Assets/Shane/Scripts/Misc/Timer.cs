using System;
using System.Collections;
using UnityEngine;

//Timer which runs until specified time and can call methods after it has reached this time.

public class Timer 
{
    private MonoBehaviour monoBehaviour;
    private float timeToStop;
    private Action[] callbacksOnTimerEnd;

    public float elapsed;
    public bool ended = false;

    public Timer(MonoBehaviour monoBehaviour,float timeToStop)
    {
        this.monoBehaviour = monoBehaviour;
        this.timeToStop = timeToStop;
    }
    public Timer(MonoBehaviour monoBehaviour, float timeToStop, params Action[] callbacksOnTimerEnd)
    {
        this.monoBehaviour = monoBehaviour;
        this.timeToStop = timeToStop;
        this.callbacksOnTimerEnd = callbacksOnTimerEnd;
    }

    public void Start() => monoBehaviour.StartCoroutine(StartTimerCoroutine(timeToStop, callbacksOnTimerEnd));

    private IEnumerator StartTimerCoroutine(float timeToStop,params Action[] callbacks)
    {
        //Increase timer until it reaches time to stop.
        while (elapsed <= timeToStop)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Callbacks if any.
        if(callbacks != null)
        {
            for (int i = 0; i < callbacks.Length; i++)
            {
                callbacks[i]?.Invoke();
            }
        }

        ended = true;
    }
}
