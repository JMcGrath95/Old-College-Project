using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float despawnTimer = 1;
    
    
    void Start()
    {
        Invoke("Destroy", despawnTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;

        if (other.TryGetComponent(out iDamageable))
        {
            iDamageable.TakeDamage(10);
            Destroy(gameObject);
            Debug.Log("Damaged Object");
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
