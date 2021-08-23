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
    public GameObject Player;
    public Enemy_Stats Enemy;
    public GameObject source;
    public string Fly = "rig_003|Flying";
  
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = gameObject.GetComponent<Enemy_Stats>();        
        attack_state.Update_Bat_Component(meshAgent, Player, Enemy, animator, this);
        movement_state.Update_Bat_Components(meshAgent, Player, Enemy, animator, this);
        ChangeState(movement_state);
    }

    
    public override void Update()
    {
        if (IsInRange())
        {
            ChangeState(attack_state);
        }
        else ChangeState(movement_state);
        base.Update();
    }
    bool IsInRange()
    {
        if (Vector3.Distance(meshAgent.transform.position, Player.transform.position) <= Enemy.Enemy.AttackRange)
        {
            return true;
        }
        else return false;
    }
    public void Bat_Spit_Attack()
    {
        Vector3 shootDir = (Player.transform.position - meshAgent.transform.position).normalized;
        GameObject Temp = Instantiate(Enemy.Enemy.projectile_prefab, gameObject.transform.position+new Vector3(0,2,0), Quaternion.identity);
        AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
        bullet.SetDir(shootDir, transform.position, Enemy.Enemy.AttackRange, (int)Enemy.Enemy.Attack, Enemy.Enemy.AttackSpeed, source.transform,transform);
        Debug.DrawRay(transform.position,shootDir,Color.yellow,Mathf.Infinity);
    }
}
