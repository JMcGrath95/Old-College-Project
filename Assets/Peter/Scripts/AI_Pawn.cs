using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Pawn : MonoBehaviour
{
    public enum Enemy_Type 
    {
        Melee,
        Range,
        Boss
    }
    public Enemy_Type _enemy_type;
    public NavMeshAgent NavMeshAgent;
    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
}
