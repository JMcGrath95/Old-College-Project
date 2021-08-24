using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_States 
{
    Spitting,
    Rolling
}
public class AI_Boss_HedgeHog : MonoBehaviour
{
    Boss_States current_state;
    public GameObject Player_Obj;
    public _boss Boss_Data;
    Collider Collider;
    Room room;
    public Bullet_Controller Bullet_Controller;
    bool 
        RolledUP,
        NeedNewTarget,
        HasHitWall;

    Animator HedgeHod_Animator;
    //ANIMATION STATES//
    //-----------------------------//
    const string RollUp = "rig_005|Hedgehog Curlup Action";
    const string UnRoll = "rig_005|Hedgehog UnCurlup Action";
    const string Roll = "Roll";
    //-----------------------------//

    //TEMP VARIABLES//
    //-----------------------------//
    public float 
        roll_count,
        velocity,
        timer;

    Vector3
        Vector_velocity,
        prev_pos,
        target_hold;
   
    public LayerMask ignore;
    
    //-----------------------------//
    private void Awake()
    {
        Player_Obj = GameObject.FindGameObjectWithTag("Player");
        HedgeHod_Animator = GetComponent<Animator>();
        //Bullet_Controller = GetComponent<Bullet_Controller>();
        current_state = Boss_States.Spitting;        
        RolledUP = false;
        HasHitWall = false;
        NeedNewTarget = true;
        Collider = GetComponent<Collider>();
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.DeathEvent += Health_DeathEvent;
        room = GetComponentInParent<Room>();
        ignore = ~ignore;
        roll_count = 0;
        //Bullet_Controller.enabled = false;
        timer = 10f;
        HedgeHod_Animator.SetBool("HasUnrolled", true);
    }
    private void Health_DeathEvent()
    {
        room.roomCleared = true;
        room.OpenDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
        velocity_track();
        HedgeHod_Animator.SetFloat("Velocity",velocity);
        HedgeHod_Animator.speed = 1f;        
        switch (current_state)
        {
            case Boss_States.Rolling:
                if (roll_count <= 3)
                {
                    if (!RolledUP)
                    {
                        StartCoroutine(RollUP());
                    }
                    if (RolledUP)
                    {
                        if (NeedNewTarget)
                        {
                            target_hold = Cast();
                            HasHitWall = false;
                        }

                        //Playing rolling animation
                        //Calling moving method                    
                        if (target_hold != Vector3.zero)
                        {
                            Move(target_hold);
                        }

                        if (Vector3.Distance(target_hold,transform.position)<=0.2f)
                        {
                            NeedNewTarget = true;
                        }
                    }
                }
                else
                    StartCoroutine(UNRoll());                   
                
                break;
            case Boss_States.Spitting:
                
                Vector3 vector = room.floor.transform.position + new Vector3(0, 3, 0);
                if (Vector3.Distance(transform.position,vector)<=0.1f)
                {
                    HedgeHod_Animator.enabled = false;
                    Stop();
                    if (timer>=0)
                    {
                        //Bullet_Controller.enabled = true;
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        timer = 10f;
                        current_state = Boss_States.Rolling;
                        //Bullet_Controller.enabled = false;
                        HedgeHod_Animator.enabled = true;
                        roll_count = 0f;
                    }
                    
                }
                else
                {
                    Move(room.floor.transform.position + new Vector3(0, 3, 0));
                }
                Debug.Log("Spitting State");
                break;
            default:
                break;
        }
    }

    void Move(Vector3 Direction)   
    {
        
        //Moving gameobject towards passed Vector3
        //Direction = new Vector3(Direction.x, 0f, Direction.z);
        Direction = Direction - transform.position;
        //transform.Translate(Direction * Time.deltaTime*0.1f);
        transform.position += Direction * (Time.deltaTime);
        Collider.isTrigger = true;
        HedgeHod_Animator.Play(Roll);
        
        
        //Looking towards the movement direction
        Vector3 MovementDir = Vector3.RotateTowards(transform.forward, Direction, 10*Time.deltaTime, 10f);
        transform.rotation = Quaternion.LookRotation(MovementDir);
    }

    IEnumerator RollUP() 
    {
        HedgeHod_Animator.Play(RollUp);
        yield return new WaitForSeconds(2f);
        current_state = Boss_States.Rolling;
        RolledUP = true;
    }
    IEnumerator UNRoll() 
    {
        //Playing UnRolling animation and changing state after animation has finished playing(Currently 4 seconds)
        HedgeHod_Animator.Play(UnRoll);
        Stop();
        StopRotation();
        yield return new WaitForSeconds(3f);
        RolledUP = false;
        HedgeHod_Animator.SetBool("HasUnrolled",true);
        current_state = Boss_States.Spitting;
    }
    void Stop() 
    {
        transform.Translate(Vector3.zero);
        Collider.isTrigger = false;
    }
    void velocity_track() 
    {
        Vector_velocity = ((transform.position - prev_pos) / Time.deltaTime).normalized;
        prev_pos = transform.position;
        velocity = Vector_velocity.magnitude;        
    }

    Vector3 Cast() 
    {
        RaycastHit hit;
        Vector3 RayDir=Player_Obj.transform.position+new Vector3(0,3,0)-transform.position;
        Vector3 TEMP_position;
        if (Physics.Raycast(transform.position, RayDir, out hit, Mathf.Infinity, ignore))
        {
            //Debug.DrawRay(transform.position, RayDir, Color.green, 30f);
            if (hit.collider.CompareTag("Wall")||hit.collider.gameObject.GetComponent<Door>()!=null)
            {
                Debug.DrawLine(transform.position, hit.point, Color.blue, 24f);
                NeedNewTarget = false;
                TEMP_position = hit.point;
                TEMP_position.y = hit.collider.gameObject.transform.position.y;
                Debug.Log("Hit Wall");
                return TEMP_position;
                
            }
            else if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player has bit Hit(FUCK THIS)");
                return Vector3.zero;
            }            
            else 
            {
                Debug.Log("Didnt Hit Shit");
                return Vector3.zero;
            } 
        }        
        else 
        {
            Debug.Log("Also Didnt Hit Shit");
            return Vector3.zero;
        } 
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(20);//To be changed after boss_data implementation
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(20);//To be changed after boss_data implementation
        }
        else if (other.gameObject.CompareTag("Wall")|| other.gameObject.GetComponent<Hallway>() != null)
        {
            HasHitWall = true;
            NeedNewTarget = true;
            roll_count++;
        }        
    }
    void StopRotation() 
    {
        HedgeHod_Animator.SetBool("NeedsStop", true);
        HedgeHod_Animator.Play("HedgeHogStanding");
    }
}
