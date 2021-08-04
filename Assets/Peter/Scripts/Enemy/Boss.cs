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
    public static event Action<_boss> BossCreatedEvent;

    public int ID;    
    public string Name;
    public float Health;
    public float Speed;
    public float Attack;
    public float AttackSpeed;
    public float AttackRange;
    public GameObject Projectile;
    public GameObject Boss_Prefab;

    public _boss(int iD, string name, float health, float speed, float attack, float attackSpeed, float attackRange, GameObject projectile, GameObject boss_Prefab)
    {
        ID = iD;
        Name = name;
        Health = health;
        Speed = speed;
        Attack = attack;
        AttackSpeed = attackSpeed;
        AttackRange = attackRange;
        Projectile = projectile;
        Boss_Prefab = boss_Prefab;

        BossCreatedEvent?.Invoke(this);
    }
}
