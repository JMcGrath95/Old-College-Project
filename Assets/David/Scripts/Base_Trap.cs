using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TrapType
{
    ContactTrap,
    TimerTrap,
    PoisonTrap
}

public class Base_Trap : MonoBehaviour
{
    
    
    Animator spikeAnimtor;

    void Start()
    {
        //spikeAnimtor.GetComponent<Animator>();
        InitializeTrapMotion();
    }

    public void InitializeTrapMotion()
    {
        //spikeAnimtor.Play("Spike_Movement", 0,0.5f);
        //spikeAnimtor.Play("MiddleSpikes", 0,0.5f);
    }

    public void DeactivateTrap()
    {
        spikeAnimtor.SetBool(name: "Spike_Movement", false);
        spikeAnimtor.SetBool(name: "MiddleSpikes", false);
        spikeAnimtor.SetBool(name: "CenterTrap", false);
    }

    public void ActivateTrap()
    {
        spikeAnimtor.SetBool(name: "Spike_Movement", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print($"ggg {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
