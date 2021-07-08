using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public iState currentState;
    public iState previousState;

    private bool InTransition;

    public void ChangeState(iState newState)  
    {
        if (currentState == newState) //Removed "or InTransition".
            return;

        ChangeStateRoutine(newState);
    }

    public void RevertState()
    {
        if (previousState != null)
            ChangeState(previousState);
    }

    public void ChangeStateRoutine(iState newState)
    {
        InTransition = true;

        if (currentState != null)
            currentState.Exit();

        previousState = currentState;
        currentState = newState;

        if(currentState != null)
            currentState.Enter();

        InTransition = false;
    }

    private void Update()
    {
        if (currentState != null && !InTransition)
            currentState.Tick();
    }
}
