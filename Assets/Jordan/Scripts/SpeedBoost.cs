using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Item
{
    

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SimplePlayerMove>().speed += value;
            Destroy(gameObject);
            Debug.Log("colliding");
        }
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if(hit.gameObject.tag == "Player")
    //    {
    //        hit.gameObject.GetComponent<SimplePlayerMove>().speed += value;
    //        Destroy(gameObject);
    //        Debug.Log("colliding");
    //    }
    //}
}
