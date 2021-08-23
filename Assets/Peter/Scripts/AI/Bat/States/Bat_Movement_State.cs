using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Bat_Movement_State : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public GameObject Player;
    public Enemy_Stats Enemy;
    private Bat_Machine_State _StateMachine;    
    public void Enter()
    {
        animator.Play(_StateMachine.Fly);
        meshAgent.isStopped = true;
    }

    public void Exit()
    {
        meshAgent.isStopped = false;
    }

    public void Tick()
    {
        Move(); 
    }
    void Move() 
    {
        meshAgent.isStopped = false;
        meshAgent.destination = Player.transform.position;
    }
    public void Update_Bat_Components(NavMeshAgent agent, GameObject player, Enemy_Stats stats, Animator Animator, Bat_Machine_State stateMachine)
    {
        meshAgent = agent;
        Player = player;
        Enemy = stats;
        animator = Animator;
        _StateMachine = stateMachine;
    }
}
