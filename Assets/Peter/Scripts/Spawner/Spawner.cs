
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject Player;
    public Enemies Enemies;
    public Boss Bosses;
    //public string SpawnList;
    public AI_Manager manager;

    
    public bool AreSpawned = false;
    
    
    
    //Spawning regular boss.
    public void Enemy_Spawn_Small(float amount_to_spawn,int Range_from,int Range_to) 
    {
        
        //Debug.Log(test);
        int[] what_to_spawn = Split(RandomSpawnOrder(amount_to_spawn, Range_from, Range_to));

        

        foreach (var enemy in what_to_spawn)
        {
            int random_int = Random.Range(0, 4);
            Vector3 Spawn_point = GetRandomSpawn(random_int).position;
            Enemy temp;
            temp = Enemies.enemies.Find(e => e.ID == enemy);
            GameObject just_made = Instantiate(temp.prefab);
            just_made.name = temp.Name;
            just_made.GetComponent<AI_Pawn>().Enemy = new Enemy
            {
                ID = temp.ID,
                prefab = temp.prefab,
                Name = temp.Name,
                Speed = temp.Speed,
                Health = temp.Health,
                Attack = temp.Attack,
                AttackRange = temp.AttackRange,
                AttackSpeed = temp.AttackSpeed,
                enemyType = temp.enemyType,
                projectile_prefab = temp.projectile_prefab
            };        
            just_made.transform.parent = manager.transform;
            just_made.transform.position = Spawn_point;            
        }
        AreSpawned = true;
    }
    //Simple method to spawn a boss
    //TO BE IMPROVED
    public void Spawn_Boss(int id) 
    {
        _boss temp = Bosses.bosses.Find(b => b.ID == id);
        GameObject just_made_boss = Instantiate(temp.Boss_Prefab);
        just_made_boss.name = temp.Name;
        just_made_boss.GetComponent<AI_Boss>().Boss = new _boss
        {
            ID=temp.ID,
            Name=temp.Name,
            Speed=temp.Speed,
            Attack=temp.Attack,
            AttackSpeed=temp.AttackSpeed,
            AttackRange=temp.AttackRange,
            Projectile=temp.Projectile,
            Boss_Prefab=temp.Boss_Prefab
        };
        
    }
    //Splits the spawn string on ","
    int[] Split(string List) 
    {
        
        string[] subs = List.Split(',');
        int[] temp=new int[subs.Length];
        for (int i = 0; i < subs.Length; i++)
        {
            temp[i] = int.Parse(subs[i]);
        }
        return temp;
    }

    //"how_many_to_spawn" is the number of enemies you want to spawn
    //"Range_form" and "Range_to" represent the range from the list of enemies to be randomised from.
    //Method randomises the enemy how many time you want and joins it as a string to be passed on to the spawner.
    string RandomSpawnOrder(float how_many_to_spawn,int Range_from, int Range_to) 
    {
        string spawn_order=null;
        for (int i = 0; i < how_many_to_spawn; i++)
        {
           int random_enemy = Random.Range(Range_from,Range_to);
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
    //Pulls all of the objects tagged as "SpawnPoint" into an array
    //Loops through it and finds a random point
    Transform GetRandomSpawn(int random_int) 
    {
        GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            if (i==random_int)
            {
                return SpawnPoints[i].transform;
            }
        }
        return null;
    }
}
