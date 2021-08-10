using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    GameObject Player;
    public GameObject chargeLinePrefab;
    Vector3 playerPos;
    

    public void FindPlayerPos(Vector3 pos)
    {
        playerPos = pos;
        
    }
    public void ChargeLine()
    {
        Instantiate(chargeLinePrefab, playerPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));      
    }
    
}
