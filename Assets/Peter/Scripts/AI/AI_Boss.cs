using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public enum Boss_States 
//{
//    Spitting,
//    Rolling,
//}

public class AI_Boss : MonoBehaviour
{
    Boss_States currentstate = Boss_States.Spitting;
    public _boss Boss;
    public GameObject Player;    
    bool IsRolling = false;
    public bool HitWall = false;
    bool CanFire = true;
    bool CanTarget;
    bool RolledUp = false;
    public Transform BulletSource;
    Room room;
    NavMeshAgent boss_agent;
    //Rigidbody rigid_body;
    Vector3 currenttarget;
    Collider _collider;
    float rollcount = 0;
    Animator animator;
    const string RollUp = "rig_005|Hedgehog Curlup Action";    
    const string UnWrapping = "rig_005|Hedgehog UnCurlup Action";
    public float rolling_speed;
    public LayerMask ignore;

    private int shoot_count;


    //Speeds for the animator
    float default_speed = 1f;
    float stop_speed = 0f;
    

    void Update()
    {        
        switch (currentstate)
        {
            case Boss_States.Spitting:                
                LookTowards();
                if (shoot_count <= 5 && CanFire==true)
                {
                    StartCoroutine(Fire());
                }
                else if (shoot_count > 5)
                {
                    currentstate = Boss_States.Rolling;
                    rollcount = 0;
                }
                //Effect
                break;
            case Boss_States.Rolling:
                if (!IsRolling && rollcount <= 3)
                {
                    StartCoroutine(RollUP());
                }
                else if (rollcount >= 3)
                {
                    StopRigidBody();
                    StartCoroutine(UnRoll());
                    currentstate = Boss_States.Spitting;
                }
                //Rotation();
                if (RolledUp&&rollcount<=3)
                {
                    Move(currenttarget);
                }
                //currenttarget = (new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(Player.transform.position.x, 0f, Player.transform.position.z)).normalized;
                //Rotation();
                //Move();                
                //LookTowards();
                Debug.Log(_collider.isTrigger.ToString());
                Debug.Log(currenttarget);
                break;            
            default:
                break;
        }
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.DeathEvent += Health_DeathEvent;
        room = GetComponentInParent<Room>();
        //rigid_body = GetComponent<Rigidbody>();        
        boss_agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        animator.speed = stop_speed;
        rolling_speed = 10f;
        CanTarget = true;
        //rigid_body.constraints = RigidbodyConstraints.FreezePositionY;
        //bullet.InitializeEffect(BulletSource.gameObject);
        //currentstate = Boss_States.Spitting;
    }
    
    private void Health_DeathEvent()
    {
        room.roomCleared = true;
        room.OpenDoors();
    }
    void Rotation() 
    {
        //rigid_body.MoveRotation(rigid_body.rotation * Quaternion.Euler(new Vector3(180, 0, 0) * Time.fixedDeltaTime * rolling_speed));
        transform.Rotate(Vector3.right * Time.deltaTime * (rolling_speed*50f));
    }
    void LookTowards()
    {
        Vector3 targetDirection = Player.transform.position - transform.position;
        Vector3 NewDir = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(NewDir);        
    }
    
    IEnumerator RollTo()
    {
        if (CanTarget)
        {   
            CanTarget = false;
            currenttarget = Cast();            
        }        
        Vector3 Player_pos = Player.transform.position;
        IsRolling = true;                
        yield return new WaitUntil(() => HitWall == true);
        IsRolling = false;
        rollcount++;
        _collider.isTrigger = false;        
        StopRigidBody();
        HitWall = false;        
        CanTarget = true;
    }     
    IEnumerator Fire() 
    {
        //bullet.TriggerEffect(); 
        CanFire = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("Fired!!!!" + shoot_count);
        CanFire = true;
        shoot_count++;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage((int)Boss.Attack);           
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage((int)Boss.Attack);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            HitWall = true;
            StopRigidBody();
            //rigid_body.isKinematic = true;
        }
        else if (other.gameObject.GetComponent<Hallway>() != null)
        {
            StopRigidBody();
            HitWall = true;
        }   
    }
    void StopRigidBody() 
    {        
        //rigid_body.velocity = new Vector3(0,0,0);
        //rigid_body.angularVelocity = new Vector3(0, 0, 0);
        //rigid_body.isKinematic = true;
        //rigid_body.isKinematic = false;
        //LookTowards();
    }

    void Move(Vector3 target) 
    {
        //transform.position += target * Time.deltaTime * rolling_speed;
        transform.Translate(target * Time.deltaTime * rolling_speed);
    }
    IEnumerator RollUP() 
    {
        animator.Play(RollUp);
        animator.speed = default_speed;
        _collider.isTrigger = true;
        yield return new WaitForSeconds(2f);
        animator.speed = stop_speed;
        RolledUp = true;
        StartCoroutine(RollTo());
    }
    IEnumerator UnRoll() 
    {
        animator.Play(UnWrapping);
        animator.speed = default_speed;
        yield return new WaitForSeconds(1f);
        RolledUp = false;
        animator.speed = stop_speed;
    }
    Vector3 Cast()
    {
        RaycastHit hit;
        Vector3 target = Player.transform.position - transform.position;
        Physics.Raycast(transform.position, target, out hit,9000f,~ignore);
        Debug.DrawRay(transform.position, target+new Vector3(0,3,0), Color.red,4000f);
        if (hit.collider.CompareTag("Wall"))
        {
            currenttarget = transform.position - hit.point;
            Destroy(hit.collider.gameObject);
            return currenttarget;
        }
        else return Vector3.zero;
    }
    
}


