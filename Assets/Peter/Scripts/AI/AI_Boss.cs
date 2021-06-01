using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Boss : MonoBehaviour
{
    public _boss Boss;
    public GameObject Player;
    float timeleft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 targetDirection = Player.transform.position - transform.position;
        
    }
    void Update()
    {
        
        LookTowards();
        if (Vector3.Distance(Player.transform.position,transform.position)<=Boss.AttackRange)
        {
            Fire();
        }
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
            Vector3 shootDir = (Player.transform.position - transform.position).normalized;
            GameObject Temp = Instantiate(Boss.Projectile, gameObject.transform.position, Quaternion.identity);
            AI_Bullet bullet = Temp.GetComponent<AI_Bullet>();
            bullet.SetDir(shootDir, transform.position, Boss.AttackRange,(int)Boss.Attack);
            timeleft = 0.5f;
        }

    }
}
