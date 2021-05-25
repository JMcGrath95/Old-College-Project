using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Boss",menuName ="Bosses")]
[Serializable]
public class Boss : ScriptableObject
{
    public List<_boss> bosses = new List<_boss>();
}
[Serializable]
public class _boss 
{
    public int ID;    
    public string Name;
    public float Health;
    public float Speed;
    public float Attack;
    public float AttackSpeed;
    public float AttackRange;
    public GameObject Projectile;
    public GameObject Boss_Prefab;
}
