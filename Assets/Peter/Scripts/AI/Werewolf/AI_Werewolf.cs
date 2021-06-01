using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Werewolf : AI_Pawn
{
    // Start is called before the first frame update
    const string MeleeAttackAnim = "Melee Hit Action 1";
    float timer = 0f;
    int mask = 0 << 0;
    
    public override void Awake()
    {
        //Debug.Log(Enemy.AttackRange.ToString());
        SetUp();
        mask = ~mask;
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        SetVelocity(NavMeshAgent.velocity.magnitude);
        if (IsInRange(transform))
        {
            NavMeshAgent.velocity = Vector3.zero;
            Werewolf_Attack();
        }
        else 
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.destination = Player.position;            
        }
    }
    
    private void Werewolf_Attack() 
    {
        RaycastHit hit;
        timer -= Time.deltaTime;
        if (timer<=0f)
        {
            
            if (Physics.Raycast(transform.position, Player.position - transform.position, out hit, Enemy.Enemy.AttackRange, mask))
            {
                if (hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage((int)Enemy.Enemy.Attack);
                    NavMeshAgent.isStopped = true;
                    animator.Play(MeleeAttackAnim);
                    timer = 2f;
                }
                else 
                {
                    NavMeshAgent.isStopped = true;
                    animator.Play(MeleeAttackAnim);
                    timer = 2f;
                }

                
            }
            
        }
    }
}
