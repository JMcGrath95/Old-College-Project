using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Werewolf_State_Attack : iState
{
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    private Werewolf_StateMachine _StateMachine;
    float timer=0f;
    public void Enter()
    {       
        animator = _StateMachine.animator;
        meshAgent = _StateMachine.meshAgent;
        Player = _StateMachine.Player;
        Enemy = _StateMachine.Enemy;
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
            if (Physics.Raycast(meshAgent.transform.position,Player.position-meshAgent.transform.position,out hit,Enemy.Enemy.AttackRange))
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
    
}
