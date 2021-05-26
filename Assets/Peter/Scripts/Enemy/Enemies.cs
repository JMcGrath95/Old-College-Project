using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum EnemyType
{
    Melee,
    Range
}
[CreateAssetMenu(fileName = "Enemies", menuName = "Enemies")]
[Serializable]
public class Enemies : ScriptableObject
{
    public List<Enemy> enemies = new List<Enemy>();
}

[Serializable]
public class Enemy
{
    public int ID;
    public float Health;
    public float Speed;
    public float Attack;
    public float AttackSpeed;
    public float AttackRange;
    public string Name;
    public GameObject prefab;
    public EnemyType enemyType;
    public GameObject projectile_prefab;    
}
