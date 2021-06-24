using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Boss : MonoBehaviour
{
    public _boss Boss;
    public GameObject Player;
    float timeleft = 0f;
    float timeleftroll = 3f;
    bool IsRolling = false;
    bool HitWall = false;
    public Transform BulletSource;
    Room room;
    NavMeshAgent boss_agent;
    Rigidbody rigid_body;
    Vector3 currenttarget;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        //TempAnimControl();        
        if (timeleftroll <= 0f && !IsRolling)
        {
            LookTowards();
            StartCoroutine(RollTo());
        }
        else if (Vector3.Distance(Player.transform.position, transform.position) <= Boss.AttackRange && !IsRolling)
        {
            LookTowards();
            Fire();
        }
        if (IsRolling)
        {
            rigid_body.MoveRotation(rigid_body.rotation * Quaternion.Euler(new Vector3(180, 0, 0) * Time.fixedDeltaTime));
        }
        timeleftroll -= Time.deltaTime;
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.DeathEvent += Health_DeathEvent;
        room = GetComponentInParent<Room>();
        rigid_body = GetComponent<Rigidbody>();        
        boss_agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Health_DeathEvent()
    {
        room.roomCleared = true;
        room.OpenDoors();
    }
    void LookTowards()
    {
        Vector3 targetDirection = Player.transform.position - transform.position;
        Vector3 NewDir = Vector3.RotateTowards(transform.forward, targetDirection, 10 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(NewDir);
    }
    void Fire()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            //rigid_body.isKinematic = true;
            Vector3 shootDir = ((Player.transform.position + new Vector3(0, 0.5f)) - BulletSource.position);
            GameObject Temp = Instantiate(Boss.Projectile, BulletSource.position,Quaternion.identity);
            AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
            bullet.SetDir(shootDir, BulletSource.position, Boss.AttackRange, (int)Boss.Attack, Boss.AttackSpeed, BulletSource);
            timeleft = 0.5f;
        }
    }
    IEnumerator RollTo()
    {
        //rigid_body.isKinematic = true;
        currenttarget = (Player.transform.position - transform.position).normalized;
        Vector3 Player_pos = Player.transform.position;
        IsRolling = true;
        rigid_body.AddForce(new Vector3(currenttarget.x, 0f ,currenttarget.z) * 180f , ForceMode.Force);
        
        //yield return new WaitUntil(() => HitWall == true);
        yield return new WaitForSeconds(10);
        timeleftroll = 10f;        
        IsRolling = false;
        //rigid_body.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage((int)Boss.Attack);
            //Add knockback
        } 
    }
    void HasHitWall() 
    {
        if (timeleftroll<=-10)
        {
            HitWall = true;
        }
        else HitWall = false;
    }
    void TempAnimControl()    
    {
        animator.SetBool("IsRolling", IsRolling);
    }
}
