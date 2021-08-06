using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventListenerRemover 
{
    public static void RemoveAllListenersFromEvent(Delegate eventToRemoveListeners)
    {
        Delegate[] listeners = eventToRemoveListeners.GetInvocationList();

        for (int i = 0; i < listeners.Length; i++)
        {
            //eventToRemoveListeners -= listeners[i];
        }


       //Debug.Log(eventType.GetInvocationList().Length);
    }
}
