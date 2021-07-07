using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Trap : Base_Trap
{
    public GameObject OuterSpikes;
    public GameObject MiddleSpikes;
    public GameObject CenterSpikes;

    

    void Start()
    {
        
    }

    public Spike_Trap()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;

        if(other.TryGetComponent(out iDamageable))
        {
            iDamageable.TakeDamage(SpikeDamage);
        }
    }

    public void OuterTrap()
    {
        
    }

    public void MidTrap()
    {

    }
}
