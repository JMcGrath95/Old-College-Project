using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;

        if (other.gameObject.TryGetComponent(out iDamageable))
        {
            iDamageable.TakeDamage(10);
        }
    }
}
