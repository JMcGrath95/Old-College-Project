using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect List", menuName = "Effects/New Effect List")]
public class SOEffectsList : ScriptableObject
{
    public List<BaseEffect> projectiles = new List<BaseEffect>();
}
