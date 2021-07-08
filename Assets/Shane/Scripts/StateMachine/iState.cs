using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iState
{
    public void Enter();
    public void Tick();
    public void Exit();
}
