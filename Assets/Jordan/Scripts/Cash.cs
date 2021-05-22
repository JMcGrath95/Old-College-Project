using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Item
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SimplePlayerMove>().cash += value;
            Destroy(gameObject);
            Debug.Log("colliding");
        }
    }
}
