using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AI_Manager : MonoBehaviour
{
    [SerializeField]
    protected GameObject Player;
    public List<AI_Pawn> list_of_pawns;
    public Spawner spawner;
    

    public List<AI_Pawn> list_of_melee_pawns;


    
    void Start()
    {
        spawner.Spawn("0,0,0,0,0,0,0");
        Player = GameObject.FindGameObjectWithTag("Player");
        list_of_pawns = GetComponentsInChildren<AI_Pawn>().ToList();
        if (list_of_pawns != null)
        {
            foreach (var enemy in list_of_pawns)
            {

                if (enemy.GetComponent<Enemy_Stats>().enemyType == EnemyType.Melee)
                {
                    list_of_melee_pawns.Add(enemy);
                }
            }
        }


        if (Player != null && list_of_melee_pawns != null)
        {
            MoveAllGeneric(list_of_melee_pawns);
        }
    }

    
    void Update()
    {

    }
    public void AddToBeManaged() 
    {
         
    }
    public void MoveAllGeneric(List<AI_Pawn> enemy_list)
    {
        foreach (var pawn in enemy_list)
        {
            pawn.NavMeshAgent.SetDestination(Player.transform.position);
        }
    }
}
