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

    private Coroutine TimerCoroutine;


    int[] testArrayGC = new int[1000];

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

        for (int i = 0; i < testArrayGC.Length; i++)
        {
            System.Random rng = new System.Random();

            testArrayGC[i] = rng.Next(0, 300);
        }
    }

    public void Start() => TimerCoroutine = monoBehaviour.StartCoroutine(StartTimerCoroutine(timeToStop, callbacksOnTimerEnd));

    public void Reset()
    {
        if(TimerCoroutine != null)
        {
            monoBehaviour.StopCoroutine(TimerCoroutine);
            TimerCoroutine = null;
        }
    }

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
