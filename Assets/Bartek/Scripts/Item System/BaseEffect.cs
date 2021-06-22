using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    public string EffectName = "New Effect";                        //used for display and can be used for searching
    public AudioClip EffectSound = null;                            //some effects can have no sound e.g passive/buff effects

    [HideInInspector]
    public GameObject Owner;                                        //reference to owner so effect will not harm owner(set through InitializeEffect not inspector)

    public abstract void InitializeEffect(GameObject owner);        //used when effects gets added when any values needs to shared from effect or to effect
    public abstract void TriggerEffect();                           //called when effect is meant to be activated and to define what happens when effect is activated
}
