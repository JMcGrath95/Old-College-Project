using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Pawn : MonoBehaviour
{    
   
    public NavMeshAgent NavMeshAgent;
    private void Start()
    {
        
    }
    public void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
}
