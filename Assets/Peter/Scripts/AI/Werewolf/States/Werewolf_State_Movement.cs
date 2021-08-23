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
        animator.Play(_StateMachine.Walking);
    }   

    public void Tick()
    {
        Move();
    }
    public void Exit()
    {
        //throw new System.NotImplementedException();
    }
    public void Move() 
    {
        meshAgent.isStopped = false;
        meshAgent.destination = Player.position;
    }
    public void UpdateComponent(NavMeshAgent agent,Transform player,Enemy_Stats stats,Animator Animator,Werewolf_StateMachine stateMachine) 
    {
        meshAgent = agent;
        Player = player;
        Enemy = stats;
        animator = Animator;
        _StateMachine = stateMachine;
    }
}
