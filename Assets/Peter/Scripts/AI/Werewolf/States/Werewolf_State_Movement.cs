using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Werewolf_State_Movement : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    private Werewolf_StateMachine _StateMachine;
    public void Enter()
    {        
        meshAgent = _StateMachine.meshAgent;
        Player = _StateMachine.Player;
        Enemy = _StateMachine.Enemy;
        animator = _StateMachine.animator;
        animator.Play(_StateMachine.Attack);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        Move();
    }
    public void Move() 
    {
        meshAgent.isStopped = false;
        meshAgent.destination = Player.position;
    }
}
