using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State 
{
    Attack,
    Walking,
    Idle
}
public class AI_Pawn : MonoBehaviour
{
    protected Enemy_Stats Enemy;
    protected NavMeshAgent NavMeshAgent;    
    protected Transform Player;
    protected Animator animator;
    //float timeleft = 0f;
    public State CurrentState;
    //const string MeleeAttackAnim = "Melee Hit Action 1";
    private void Start()
    {
        
    }
    public virtual void Awake()
    {

        
        //Initial Move after spawn
        //NavMeshAgent.destination = Player.position;
        //animator.SetBool("IsAttacking", false);
        
    }
    private void Update()
    {
        
        //SetVelocity(NavMeshAgent.velocity.magnitude);
        //Debug.Log(Enemy.Name + " " + NavMeshAgent.velocity.magnitude);
        //Looking towards the player
        //Rotation();
        //Switch statement to either fire or hit.
        //if (IsInRange())
        //{
        //    NavMeshAgent.isStopped = true;
        //    NavMeshAgent.velocity = Vector3.zero;
        //    switch (Enemy.enemyType)
        //    {
        //        case EnemyType.Melee:
        //            //Melee Attack                                                           
        //            Attack();
        //            Debug.Log("Melee Attack");                    
        //            break;
        //        case EnemyType.Range:
        //            //Range Attack                    
        //            Fire();
        //            Debug.Log("Range Attack");                   
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (!IsInRange())
        //{
        //    SetAnimToAttack(false);
        //    NavMeshAgent.isStopped = false;
        //    NavMeshAgent.destination = Player.position;
        //    CurrentState = State.Walking;
        //}
        
            
        
        //else if (NavMeshAgent.isStopped==true&&!IsInRange())
        //{
        //    NavMeshAgent.destination = Player.position;
        //    NavMeshAgent.isStopped = false;
            
        //}
        //else if (HasReachedDestination())
        //{
        //    NavMeshAgent.isStopped = true;
        //}
        //else
        //{
        //    NavMeshAgent.destination = Player.position;
            
        //}
        //if (NavMeshAgent.velocity.magnitude <= 0.5f&&!animator.GetBool("IsAttacking")&&!IsInRange())
        //{
        //    NavMeshAgent.isStopped = true;
        //    animator.SetBool("IsAttacking", false);           
        //}
    }
    //Method to check the fin the player object is within the range.
    protected bool IsInRange(Transform transform) 
    {
        if (Vector3.Distance(transform.position, Player.position) <= Enemy.Enemy.AttackRange)
        {            
            return true;
        }
        else return false;
    }
    protected void SetUp()
    {
        animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = gameObject.GetComponent<Enemy_Stats>();
    }
    protected void Rotation()
    {
        Vector3 targetDirection = Player.transform.position - transform.position;
        Vector3 NewDir = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(NewDir);
    }
    //Sample fire method, will be reworked.
    //void Fire() 
    //{
    //    //SetAnimToAttack(true);
    //    animator.Play(MeleeAttackAnim);
    //    NavMeshAgent.isStopped = true;
    //    timeleft -= Time.deltaTime;
    //    if (timeleft<=0)
    //    {
    //        Vector3 shootDir = (Player.position - transform.position).normalized;
    //        GameObject Temp = Instantiate(Enemy.projectile_prefab, gameObject.transform.position, Quaternion.identity);
    //        AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
    //        bullet.SetDir(shootDir,transform.position,Enemy.AttackRange);
    //        timeleft = 2f;
    //    }

    //}
    //void Attack() 
    //{
    //    animator.Play(MeleeAttackAnim);
    //    NavMeshAgent.isStopped = true;
    //    //SetAnimToAttack(true);
    //}  

    void SetAnimToAttack(bool tof) 
    {
        animator.SetBool("IsAttacking", tof);
        CurrentState = State.Attack;
    }
    protected void SetVelocity(float speed) 
    {
        animator.SetFloat("Velocity", speed);        
    }
}
