using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    GameObject Player;
    public ChargeLine chargeLinePrefab;
    MeshRenderer mesh;
    Vector3 playerPos;
    private void Start()
    {

        
        FindPlayerPos();
        SpawnTrain(ChargeLineSpawn());
        
    }
    private void Update()
    {
        
    }
    public void FindPlayerPos()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        playerPos = p.transform.position;
        if (p != null)
            Debug.Log("PLayer Found");
        
    }
    public ChargeLine ChargeLineSpawn()
    {
        ChargeLine pre = Instantiate(chargeLinePrefab, playerPos,Quaternion.Euler(new Vector3(0,Random.Range(0f,360f),0)));

        
        return pre;

    }
    public void SpawnTrain(ChargeLine c)
    {
        gameObject.transform.position = c.Point1.transform.position;
        Debug.Log("Point1");
        gameObject.transform.LookAt(c.Point2.transform, Vector3.left);
        Debug.Log("Looking At");
        gameObject.transform.position = Vector3.Lerp(c.Point1.transform.position, c.Point2.transform.position, .5f);
        Debug.Log("Lerped");
        Destroy(c);
       
    }
    
}
