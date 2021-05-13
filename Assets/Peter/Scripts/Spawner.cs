using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject Player;
    public Enemies Enemies;
    //public string SpawnList;
    public AI_Manager manager;

    public void Spawn(string EnemyList) 
    {
        Vector3 Spawn_point;
        int[] what_to_spawn = Split(EnemyList);
        
        foreach (var enemy in what_to_spawn)
        {
            Spawn_point = GetSpawnPoint();
            Enemy temp;
            temp=Enemies.enemies.Find(e => e.ID == enemy);
            GameObject just_made= Instantiate(temp.prefab);
            just_made.name = temp.Name;
            Enemy_Stats stats=just_made.AddComponent<Enemy_Stats>();
            stats = new Enemy_Stats
            {
                Name = temp.Name,
                Speed = temp.Speed,
                Health = temp.Health,
                Attack = temp.Attack,
                AttackRange = temp.AttackRange,
                AttackSpeed=temp.AttackSpeed,
                enemyType=temp.enemyType,
            };
            just_made.transform.parent = manager.transform;
            just_made.transform.position = Spawn_point;            
        }
    }
    public Vector3 GetSpawnPoint() 
    {        
        Vector3 randomDir = Random.insideUnitSphere;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, 10f, 1);
        Vector3 final_pos = hit.position;
        return final_pos;
    }
    public int[] Split(string List) 
    {
        
        string[] subs = List.Split(',');
        int[] temp=new int[subs.Length];
        for (int i = 0; i < subs.Length; i++)
        {
            temp[i] = int.Parse(subs[i]);
        }
        return temp;
    }
}
