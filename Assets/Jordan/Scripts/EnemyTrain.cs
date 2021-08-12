using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    GameObject Player;
    public ChargeLine chargeLinePrefab;
   
    MeshRenderer mesh;
    Vector3 pPos;
    float Starttime, ToDis;
    bool repeat = false;
    public float speed = 1.0f;
    
     
    
   
    IEnumerator Start ()
    {
        Starttime = Time.time;
        
        FindPlayerPos();
            yield return SpawnLine();
        
        
    }
    private void Update()
    {
        
    }
    public void FindPlayerPos()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        
        if (p != null)
            Debug.Log("PLayer Found");

        pPos = p.transform.position;


    }
    IEnumerator SpawnLine()
    {
        ChargeLine c = Instantiate(chargeLinePrefab,pPos,Quaternion.Euler(new Vector3(0,Random.Range(0f,360f),0)));
        Vector3 a = c.Point1.transform.position;
        Vector3 b = c.Point2.transform.position;
        ToDis = Vector3.Distance(c.Point1.transform.position, c.Point2.transform.position);
        StartCoroutine(RunCharge(2,a,b));
        yield return null;
    }
    
    
    IEnumerator RunCharge(float time,Vector3 a,Vector3 b)
    {
        if(GameObject.FindGameObjectWithTag("Line"))
        {
            ChargeLine c = GameObject.FindGameObjectWithTag("Line").GetComponent<ChargeLine>();
            float i = 0.0f;
            float rate = (1.0f / time) * speed;
            while(i < 1.0f)
            {
                i += Time.deltaTime * rate;
                this.transform.position = Vector3.Lerp(a, b, 1);
                
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
    }
    
}
