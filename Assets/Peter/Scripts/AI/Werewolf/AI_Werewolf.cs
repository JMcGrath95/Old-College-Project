using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Werewolf : AI_Pawn
{
    // Start is called before the first frame update
    const string MeleeAttackAnim = "Melee Hit Action 1";
    

    public override void Awake()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        SetVelocity(NavMeshAgent.velocity.magnitude);
        if (IsInRange())
        {
            Werewolf_Attack();
        }
        else 
        {
            NavMeshAgent.destination = Player.position;            
        }
    }
    private void Werewolf_Attack() 
    {
        NavMeshAgent.isStopped = true;
        animator.Play(MeleeAttackAnim);
    }
}
