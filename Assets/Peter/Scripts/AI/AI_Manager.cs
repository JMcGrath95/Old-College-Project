using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using System;

public class AI_Manager : MonoBehaviour
{
    public Transform Player;
    //Vector3 Player_position;
    public List<AI_Pawn> list_of_pawns;
    public Spawner spawner;
    public static AI_Manager current;
    

    public List<AI_Pawn> list_of_melee_pawns;
    public List<AI_Pawn> list_of_range_pawns;


    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //Call to spawn enemies and the boss.
        StartCoroutine(SpawnEnemiesTemp());   
    }

    
    void Update()
    {        
        //if (spawner.AreSpawned)
        //{
        //    StartCoroutine(IsItInRange());
        //}
    }
    //private void Awake()
    //{
    //    current = this;
    //}
    
    //public void AddToBeManaged() 
    //{
        //list_of_pawns = GetComponentsInChildren<AI_Pawn>().ToList();
        //if (list_of_pawns != null)
        //{
        //    foreach (var enemy in list_of_pawns)
        //    {
        //        switch (enemy.GetComponent<AI_Pawn>().Enemy.enemyType)
        //        {
        //            case EnemyType.Melee:
        //                list_of_melee_pawns.Add(enemy);
        //                enemy.NavMeshAgent.destination=Player.transform.position;
        //                break;
        //            case EnemyType.Range:
        //                list_of_range_pawns.Add(enemy);
        //                enemy.NavMeshAgent.SetDestination(Player.transform.position);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}        
    //}
    //public void MoveAllGeneric(List<AI_Pawn> enemy_list)
    //{
    //    foreach (var pawn in enemy_list)
    //    {
    //        Debug.Log(pawn.NavMeshAgent.isStopped);
    //        pawn.NavMeshAgent.SetDestination(Player.transform.position);            
    //    }
    //}
    //void IsInRange() 
    //{
    //    foreach (var enemy in list_of_pawns)
    //    {
    //        if (Vector3.Distance(Player_position,enemy.transform.position) <= enemy.GetComponent<Enemy_Stats>().AttackRange)
    //        {
    //            enemy.NavMeshAgent.isStopped=true;
    //            switch (enemy.GetComponent<Enemy_Stats>().enemyType)
    //            {
    //                case EnemyType.Melee:
    //                    DummyMeleeAttack();
    //                    break;
    //                case EnemyType.Range:
    //                    DummyRangeAttack();
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
        
    //}
    //IEnumerator IsItInRange()
    //{
    //    foreach (var enemy in list_of_pawns)
    //    {
    //        if (Vector3.Distance(Player.transform.position, enemy.transform.position) <= enemy.GetComponent<AI_Pawn>().Enemy.AttackRange)
    //        {
    //            enemy.NavMeshAgent.isStopped = true;
    //            switch (enemy.GetComponent<AI_Pawn>().Enemy.enemyType)
    //            {
    //                case EnemyType.Melee:
    //                    DummyMeleeAttack();
    //                    break;
    //                case EnemyType.Range:
    //                    DummyRangeAttack();
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (enemy.NavMeshAgent.isStopped == true && Vector3.Distance(Player.transform.position, enemy.transform.position) >= enemy.GetComponent<AI_Pawn>().Enemy.AttackRange) 
    //        {
    //            Debug.Log("RANGE CHECK"+Player.transform.position);
    //            enemy.NavMeshAgent.isStopped = false;
    //            enemy.NavMeshAgent.destination=Player.transform.position;
    //        }
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //}
    //void DummyMeleeAttack() 
    //{
    //    //Play Animation
    //    //Check Weapon Collider
    //    //If hit remove health
    //    Debug.Log("Melee Hit!");
    //}
    //void DummyRangeAttack() 
    //{
    //    //Play firing animation
    //    //Instantiate projectile
    //    //Remove health on hit
    //    Debug.Log("Projectile Fired!");
    //}

    IEnumerator SpawnEnemiesTemp() 
    {
        spawner.Spawn_Boss(0);
        spawner.Enemy_Spawn_Small(10,0,2);
        yield return new WaitUntil(()=>spawner.AreSpawned==true);
        //AddToBeManaged();
    }
}
