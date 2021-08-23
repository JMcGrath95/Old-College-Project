using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float despawnTimer = 1;
    GameObject controller;
    
    void Start()
    {
        Invoke("Destroy", despawnTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != controller)
        {
            iDamageable iDamageable;

            if (other.TryGetComponent(out iDamageable))
            {
                iDamageable.TakeDamage(3);
                Destroy(gameObject);
                Debug.Log("Damaged Object");
            }
        }
       
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetController(GameObject gameObject)
    {
        controller = gameObject;
    }
}
