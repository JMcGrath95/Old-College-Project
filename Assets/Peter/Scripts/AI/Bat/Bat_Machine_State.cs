using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bat_Machine_State : StateMachine
{
    public Bat_Movement_State movement_state;
    public Bat_Attack_State attack_state;
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    public string Fly = "rig_003|Flying";
  
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = gameObject.GetComponent<Enemy_Stats>();
        ChangeState(movement_state);
    }

    
    void Update()
    {
        if (IsInRange())
        {
            ChangeState(attack_state);
        }
        else ChangeState(movement_state);
    }
    bool IsInRange()
    {
        if (Vector3.Distance(meshAgent.transform.position, Player.position) <= Enemy.Enemy.AttackRange)
        {
            return true;
        }
        else return false;
    }
    public void Bat_Spit_Attack()
    {  
            Vector3 shootDir = (Player.position - transform.position).normalized;
            GameObject Temp = Instantiate(Enemy.Enemy.projectile_prefab, gameObject.transform.position, Quaternion.identity);
            AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
            bullet.SetDir(shootDir, transform.position, Enemy.Enemy.AttackRange, (int)Enemy.Enemy.Attack, Enemy.Enemy.AttackSpeed, transform);
    }
}
