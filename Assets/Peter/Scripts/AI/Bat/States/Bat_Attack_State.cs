using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Bat_Attack_State : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public GameObject Player;
    public Enemy_Stats Enemy;
    private Bat_Machine_State _StateMachine;
    float timer = 1f;
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
        LookAt();
        timer -= Time.deltaTime;
        if (timer<=0f)
        {
            _StateMachine.Bat_Spit_Attack();
            timer = 5f;
        }       
    }

    public void Update_Bat_Component(NavMeshAgent agent, GameObject player, Enemy_Stats stats, Animator Animator, Bat_Machine_State stateMachine)
    {
        meshAgent = agent;
        Player = player;
        Enemy = stats;
        animator = Animator;
        _StateMachine = stateMachine;
    }
    void LookAt() 
    {
        meshAgent.isStopped = true;
        Vector3 targetDirection = Player.transform.position - meshAgent.transform.position;
        Vector3 NewDir = Vector3.RotateTowards(meshAgent.transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);
        meshAgent.transform.rotation = Quaternion.LookRotation(NewDir);
    }
}
