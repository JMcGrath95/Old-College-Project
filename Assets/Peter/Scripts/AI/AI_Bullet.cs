using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private Vector3 origin;
    private float range;
    float distance_traveled;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetDir(Vector3 shootDir,Vector3 origin,float range) 
    {
        this.shootDir = shootDir;
        this.origin = origin;
        this.range = range;
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
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
    float GetAngle(Vector3 dir) 
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }    
}
