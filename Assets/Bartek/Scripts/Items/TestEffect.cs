using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Effect" ,menuName = "Effects/New Effect")]
public class TestEffect : BaseEffect
{
    public override void Initiliaze(GameObject obj)
    {
        Debug.Log(EffectName + " effect initiliazed");
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered");
    }
}
