using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Pawn : MonoBehaviour
{    
   
    public NavMeshAgent NavMeshAgent;
    public Enemy Enemy;
    public Transform Player;
    float timeleft = 0f;
    private void Start()
    {
        
    }
    public void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //Initial Move after spawn
        NavMeshAgent.destination = Player.position;
    }
    private void Update()
    {
        //Looking towards the player
        Vector3 targetDirection = Player.position - transform.position;
        Vector3 NewDir = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);        
        transform.rotation = Quaternion.LookRotation(NewDir);
        //Switch statement to either fire or hit.
        if (IsInRange())
        {
            switch (Enemy.enemyType)
            {
                case EnemyType.Melee:
                    //Melee Attack
                    Debug.Log("Melee Attack");
                    break;
                case EnemyType.Range:
                    //Range Attack
                    Fire();
                    Debug.Log("Range Attack");
                    break;
                default:
                    break;
            }
        }
        else if (NavMeshAgent.isStopped==true)
        {
            NavMeshAgent.destination = Player.position;
            NavMeshAgent.isStopped = false;
        }
        else
        {
            NavMeshAgent.destination = Player.position;
        }
    }
    //Method to check the fin the player object is within the range.
    bool IsInRange() 
    {
        if (Vector3.Distance(NavMeshAgent.transform.position, Player.position) <= Enemy.AttackRange)
        {
            NavMeshAgent.isStopped = true;
            return true;
        }
        else return false;
    }
    //Sample fire method, will be reworked.
    void Fire() 
    {
        timeleft -= Time.deltaTime;
        if (timeleft<=0)
        {
            Vector3 shootDir = (Player.position - transform.position).normalized;
            GameObject Temp = Instantiate(Enemy.projectile_prefab, gameObject.transform.position, Quaternion.identity);
            AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
            bullet.SetDir(shootDir,transform.position,Enemy.AttackRange);
            timeleft = 2f;
        }
        
    }
}
