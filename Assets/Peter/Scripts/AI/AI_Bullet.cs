using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private Vector3 origin;
    private float range;
    private int damage;
    Transform source2;
    float distance_traveled;
    float attack_speed;
    Transform source;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

    }
    public void SetDir(Vector3 shootDir, Vector3 origin, float range, int damage, float attack_speed, Transform source,Transform source2)
    {
        this.shootDir = shootDir;
        this.origin = origin;
        this.range = range;
        this.damage = damage;
        this.attack_speed = attack_speed;
        this.source = source;
        this.source2 = source2;
        transform.eulerAngles = new Vector3(0, 0, GetAngle(shootDir));
    }
    // Update is called once per frame
    void Update()
    {
        distance_traveled = Vector3.Distance(origin, transform.position);
        if (distance_traveled >= range)
        {
            Destroy(gameObject);
        }
        transform.position += (shootDir /*+ new Vector3(0, 0.2f)*/) * attack_speed * Time.deltaTime;
        
    }
    float GetAngle(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            Destroy(this);
        }
        //else if (collision.gameObject.transform != source||collision.gameObject.transform!=source2)
        //{
        //    Destroy(gameObject);
        //}
        //else if (collision.collider.tag=="Enemy" && collision.gameObject.transform != source)
        //{
        //    collision.collider.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(damage);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}


        //}
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(damage);
        //        Destroy(gameObject);
        //    }
        //    else if (other.gameObject.transform != source)
        //    {
        //        Destroy(gameObject);
        //    }
        //    else if (other.gameObject.tag == "Enemy" && other.gameObject.transform != source)
        //    {
        //        other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(damage);
        //        Destroy(gameObject);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<iDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(this);
        }
    }
}
