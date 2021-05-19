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
    public List<AI_Pawn> list_of_range_pawns;


    
    void Start()
    {
        StartCoroutine(SpawnEnemiesTemp());
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {

    }
    public void AddToBeManaged() 
    {
        list_of_pawns = GetComponentsInChildren<AI_Pawn>().ToList();
        if (list_of_pawns != null)
        {
            foreach (var enemy in list_of_pawns)
            {

                if (enemy.GetComponent<Enemy_Stats>().enemyType == EnemyType.Melee)
                {
                    list_of_melee_pawns.Add(enemy);
                }
                else if (enemy.GetComponent<Enemy_Stats>().enemyType == EnemyType.Range)
                {
                    list_of_range_pawns.Add(enemy);
                }
            }
        }


        if (Player != null && list_of_melee_pawns != null)
        {
            MoveAllGeneric(list_of_melee_pawns);
        }
        if (Player != null && list_of_range_pawns != null)
        {
            MoveAllGeneric(list_of_range_pawns);
        }
    }
    public void MoveAllGeneric(List<AI_Pawn> enemy_list)
    {
        foreach (var pawn in enemy_list)
        {
            pawn.NavMeshAgent.SetDestination(Player.transform.position);
        }
    }
    IEnumerator SpawnEnemiesTemp() 
    {
        spawner.Spawn(10);
        yield return new WaitUntil(()=>spawner.AreSpawned==true);
        AddToBeManaged();
    }
}
