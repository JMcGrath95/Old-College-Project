
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
    public Transform[] spawn_points;
    public bool AreSpawned = false;
    

    public void Spawn(float amount_to_spawn) 
    {
        string test = RandomSpawnOrder(amount_to_spawn);
        Debug.Log(test);
        int[] what_to_spawn = Split(test);
        
        
        
        foreach (var enemy in what_to_spawn)
        {
            int random_int = Random.Range(0, 3);
            Vector3 Spawn_point= new Vector3(0, 0, 0);
            for (int i = 0; i < spawn_points.Length; i++)
            {
                if (i==random_int)
                {
                    Spawn_point = spawn_points[i].position;
                }                
            }
            
            Enemy temp;
            temp=Enemies.enemies.Find(e => e.ID == enemy);
            GameObject just_made= Instantiate(temp.prefab);
            just_made.name = temp.Name;
            Enemy_Stats stats=just_made.AddComponent<Enemy_Stats>();

            stats.Name = temp.Name;
            stats.Speed = temp.Speed;
            stats.Health = temp.Health;
            stats.Attack = temp.Attack;
            stats.AttackRange = temp.AttackRange;
            stats.AttackSpeed =temp.AttackSpeed;
            stats.enemyType =temp.enemyType;

            just_made.transform.parent = manager.transform;
            just_made.transform.position = Spawn_point;            
        }
        AreSpawned = true;
    }
    //public Vector3 GetSpawnPoint() 
    //{        
    //    Vector3 randomDir = Random.insideUnitSphere;
    //    NavMeshHit hit;
    //    NavMesh.SamplePosition(randomDir, out hit, 10, 1);
    //    Vector3 final_pos = hit.position;
    //    return final_pos;
    //}
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

    public string RandomSpawnOrder(float how_many_to_spawn) 
    {
        string spawn_order=null;
        for (int i = 0; i < how_many_to_spawn; i++)
        {
           int random_enemy = Random.Range(0,Enemies.enemies.Count);
           if (i==how_many_to_spawn-1)
           {
                spawn_order = spawn_order + random_enemy;
           }
           else if(i==0)
           {
                spawn_order = random_enemy + ",";
           }
           else
           {
                spawn_order = spawn_order+ random_enemy +  ",";
           }
        }
        return spawn_order;        
    }
}
