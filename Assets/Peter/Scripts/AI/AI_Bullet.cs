using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private Vector3 origin;
    private float range ;
    private int damage;
    float distance_traveled;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }
    public void SetDir(Vector3 shootDir,Vector3 origin,float range,int damage) 
    {
        this.shootDir = shootDir;
        this.origin = origin;
        this.range = range;
        this.damage = damage;
        transform.eulerAngles = new Vector3(0, 0, GetAngle(shootDir));
        //Destroy(gameObject,3f);
    }
    // Update is called once per frame
    void Update()
    {
        distance_traveled = Vector3.Distance(origin,transform.position);
        if (distance_traveled>=range)
        {
            Destroy(gameObject);
        }
        float moveSpeed = 10f;
        transform.position += (shootDir + new Vector3(0,0.2f)) * moveSpeed * Time.deltaTime;
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
        if (collision.gameObject.tag=="Player")
        {
            collision.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        

    }
}
