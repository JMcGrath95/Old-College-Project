using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikes : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;

        if(other.TryGetComponent(out iDamageable))
        {
            iDamageable.TakeDamage(damage);
        }
    }
}
