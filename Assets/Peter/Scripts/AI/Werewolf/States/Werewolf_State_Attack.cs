using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Werewolf_State_Attack : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    private Werewolf_StateMachine _StateMachine;
    public LayerMask mask;
    float timer=0f;
    public void Enter()
    {  
        animator.Play(_StateMachine.Attack);
        meshAgent.isStopped = true;
    }

    public void Exit()
    {
        meshAgent.isStopped = false;
    }

    public void Tick()
    {
        RaycastHit hit;
        timer -= Time.deltaTime;
        if (timer<=0f)
        {
            if (Physics.Raycast(meshAgent.transform.position,Player.position-meshAgent.transform.position,out hit,Enemy.Enemy.AttackRange,mask))
            {
                if (hit.collider.tag=="Player")
                {
                    hit.collider.gameObject.GetComponent<iDamageable>().TakeDamage((int)Enemy.Enemy.Attack);
                    meshAgent.isStopped = true;                    
                    timer = 2f;
                }
            }
        }
    }
    public void UpdateComponents(NavMeshAgent agent, Transform player, Enemy_Stats stats, Animator Animator, Werewolf_StateMachine stateMachine) 
    {
        meshAgent = agent;
        Player = player;
        Enemy = stats;
        animator = Animator;
        _StateMachine = stateMachine;
    }
}
