using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bat : AI_Pawn
{
    // Start is called before the first frame update
    const string MeleeAttackAnim = "Melee Hit Action 1";
    float timer = 0f;
    void Start()
    {
        
    }
    public override void Awake()
    {
        //Debug.Log(Enemy.AttackRange.ToString());
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        SetVelocity(NavMeshAgent.velocity.magnitude);
        if (IsInRange(transform))
        {
            Bat_Spit_Attack();
        }
        else
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.destination = Player.position;
        }
    }
    private void Bat_Spit_Attack() 
    { 
        NavMeshAgent.isStopped = true;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 shootDir = (Player.position - transform.position).normalized;
            GameObject Temp = Instantiate(Enemy.Enemy.projectile_prefab, gameObject.transform.position, Quaternion.identity);
            AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
            bullet.SetDir(shootDir, transform.position, Enemy.Enemy.AttackRange,(int)Enemy.Enemy.Attack);
            animator.Play(MeleeAttackAnim);
            timer = 2f;
        }
    }
}