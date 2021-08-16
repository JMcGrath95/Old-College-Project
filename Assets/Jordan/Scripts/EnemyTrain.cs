using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    GameObject Player;
    public ChargeLine chargeLinePrefab;
    public float timer;
    MeshRenderer mesh;
    Vector3 pPos,starpos= new Vector3(-1000,-1000,-1000);
    float Starttime;
    bool reset = false;
    public float speed = 1.0f;
    
     
    
   
    IEnumerator Start ()
    {
        gameObject.transform.position = starpos;
        Starttime = Time.time;
       
        FindPlayerPos();
        yield return SpawnLine(timer);
        
        
    }
    private void Update()
    {
        
        
            StartCoroutine(ResetAll());
        
    }
    public void FindPlayerPos()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        
        if (p != null)
            Debug.Log("PLayer Found");

        pPos = p.transform.position;


    }
    IEnumerator SpawnLine(float time)
    {
        ChargeLine c = Instantiate(chargeLinePrefab,pPos,Quaternion.Euler(new Vector3(90,Random.Range(0f,360f),0)));
        Vector3 a = c.Point1.transform.position;
        Vector3 b = c.Point2.transform.position;
        yield return new WaitForSeconds(time);
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
                gameObject.transform.LookAt(c.Point2.transform.position);
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
                i += Time.deltaTime * rate;
                this.transform.position = Vector3.Lerp(a, b, i);
                
                yield return null;
            }
            reset = true;
        }
        else
        {
            yield return null;
        }
    }
    IEnumerator ResetAll()
    {
        if(reset)
        {
            gameObject.transform.position = starpos;
            Destroy(GameObject.FindGameObjectWithTag("Line"));
            reset = false;
        }
        yield return null;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
