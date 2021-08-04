using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Boss_States 
{
    Spitting,
    Rolling,
    Flipped,
    Leaping
}

public class AI_Boss : MonoBehaviour
{
    Boss_States currentstate = Boss_States.Spitting;
    public _boss Boss;
    public GameObject Player;
    //float timeleft = 0f;
    public ProjectileEffect bullet;
    
    bool IsRolling = false;
    bool HitWall = false;
    bool CanFire = true;
    bool IsFlipped = false;
    public Transform BulletSource;
    Room room;
    NavMeshAgent boss_agent;
    Rigidbody rigid_body;
    Vector3 currenttarget;
    Collider _collider;
    float shoot_count=0;
    float rollcount = 0;
    Animator animator;
    const string RollUp = "RollUpAnim";
    const string Standing = "StandingAnim";
    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        //Nova Spit(Bart's Effects)             <<
        //Roll                                  ##
        
        //Gets up                               ##
        //Jumps up                              ##
        //Lands in the middle                   >>
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
                if (!IsRolling&&rollcount<=3)
                {
                    animator.Play(RollUp);
                    StartCoroutine(RollTo());                    
                }
                Rotation();
                Move(currenttarget);
                break;
            case Boss_States.Flipped:
                if (!IsFlipped)
                {
                    //StartCoroutine(Flip());
                }
                //Flip
                break;
            case Boss_States.Leaping:
                Move((room.floor.transform.position - transform.position).normalized);
                if (Vector3.Distance(room.floor.transform.position,transform.position)<=0.01f)
                {
                    StopRigidBody();
                    LookTowards();
                    currentstate = Boss_States.Spitting;
                }
                //StartCoroutine(Leaping());
                //shoot_count = 0f; 
                //Leap
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
        rigid_body = GetComponent<Rigidbody>();        
        boss_agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        animator.Play(Standing);
        //bullet.InitializeEffect(BulletSource.gameObject);
        //currentstate = Boss_States.Spitting;
    }
    //IEnumerator Flip() 
    //{   
    //    IsFlipped = true;
    //    StopRigidBody();        
    //    yield return new WaitForSeconds(5f);
    //    IsFlipped = false;
    //    currentstate = Boss_States.Leaping;
    //}
    private void Health_DeathEvent()
    {
        room.roomCleared = true;
        room.OpenDoors();
    }
    void Rotation() 
    {
        rigid_body.MoveRotation(rigid_body.rotation * Quaternion.Euler(new Vector3(180, 0, 0) * Time.fixedDeltaTime * Boss.Speed / 5f));
    }
    void LookTowards()
    {
        Vector3 targetDirection = Player.transform.position - transform.position;
        Vector3 NewDir = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(NewDir);        
    }
    //void Fire()
    //{
    //    timeleft -= Time.deltaTime;
    //    if (timeleft <= 0)
    //    {
    //        Vector3 shootDir = ((Player.transform.position + new Vector3(0, 0.5f)) - BulletSource.position);
    //        GameObject Temp = Instantiate(Boss.Projectile, BulletSource.position,Quaternion.identity);
    //        AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
    //        bullet.SetDir(shootDir, BulletSource.position, Boss.AttackRange, (int)Boss.Attack, Boss.AttackSpeed, BulletSource);
    //        timeleft = 0.5f;
    //    }
    //}
    IEnumerator RollTo()
    {
        _collider.isTrigger = true;
        rigid_body.isKinematic = false;
        currenttarget = (Player.transform.position - transform.position).normalized;
        Vector3 Player_pos = Player.transform.position;
        IsRolling = true;
        //rigid_body.AddForce(new Vector3(currenttarget.x, 0f ,currenttarget.z) * 180f , ForceMode.Force);
        //rigid_body.MovePosition(transform.position + currenttarget * Time.deltaTime * 1000);        
        LookTowards();        
        yield return new WaitUntil(() => HitWall == true);
        IsRolling = false;
        rollcount++;
        _collider.isTrigger = false;
        IsFlipped = false;
        StopRigidBody();
        HitWall = false;        
        //currentstate = Boss_States.Flipped;
        
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
    //IEnumerator Leaping() 
    //{
    //    rigid_body.isKinematic = true;
    //    transform.position = room.transform.position + new Vector3(0, 2.5f, 0);
    //    currentstate = Boss_States.Spitting;
    //    StopRigidBody();
    //    yield return new WaitUntil(() => Vector3.Distance(room.floor.transform.position, transform.position) <= 0.2f);
    //    rigid_body.isKinematic = false;
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage((int)Boss.Attack);           
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage((int)Boss.Attack);
        }
        else if (other.gameObject.tag=="Wall")
        {
            HitWall = true;
            StopRigidBody();
        }
        else if (other.gameObject.GetComponent<Hallway>() != null)
        {
            StopRigidBody();
            HitWall = true;
        }   
    }
    void StopRigidBody() 
    {        
        rigid_body.velocity = new Vector3(0,0,0);
        rigid_body.angularVelocity = new Vector3(0, 0, 0);
        rigid_body.isKinematic = true;
        LookTowards();
    }

    void Move(Vector3 player_pos) 
    {
        Vector3 _player_pos = player_pos;
        rigid_body.MovePosition(transform.position + _player_pos * Time.deltaTime * Boss.Speed);
    }    
}
