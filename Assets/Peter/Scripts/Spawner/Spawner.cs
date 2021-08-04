
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject Player;
    public Enemies Enemies;
    public Boss Bosses;

    public Room roomAssignedTo;

    List<Vector3> spawnPositions = new List<Vector3>();
    List<EnemyHealth> spawnedEnemies = new List<EnemyHealth>();
    int enemiesCount;

    public bool AreSpawned = false;

    GameObject spawnedBoss;

    void GetEnemies()
    {
        foreach (EnemyHealth eh in spawnedEnemies)
        {
            enemiesCount++;
            eh.DeathEvent += Eh_DeathEvent;
        }
    }

    void GetBoss(GameObject boss)
    {
        spawnedBoss = boss;
        spawnedBoss.GetComponent<EnemyHealth>().DeathEvent += Boss_DeathEvent;
    }

    private void Boss_DeathEvent()
    {
        roomAssignedTo.roomCleared = true;
        roomAssignedTo.OpenDoors();
        ExitPortal portal = GameObject.FindGameObjectWithTag("RoomController").GetComponent<RoomController>().portalPrefab;
        Instantiate(portal, roomAssignedTo.transform);
    }

    private void Eh_DeathEvent()
    {
        enemiesCount--;

        if (enemiesCount <= 0)
        {
            roomAssignedTo.roomCleared = true;
            roomAssignedTo.OpenDoors();
        }
    }

    //Spawning regular
    void Enemy_Spawn_Small(float amount_to_spawn, int Range_from, int Range_to)
    {
        if (amount_to_spawn > 9)
        {
            amount_to_spawn = 5;
        }

        //Debug.Log(test);
        int[] what_to_spawn = Split(RandomSpawnOrder(amount_to_spawn, Range_from, Range_to));



        foreach (var enemy in what_to_spawn)
        {
            int random_int = Random.Range(0, 4);
            Vector3 Spawn_point = GetRandomPosition();
            Enemy temp;
            temp = Enemies.enemies.Find(e => e.ID == enemy);
            GameObject just_made = Instantiate(temp.prefab,Spawn_point,Quaternion.identity);
            just_made.name = temp.Name;
            Enemy_Stats stats = just_made.GetComponent<Enemy_Stats>();
            just_made.GetComponent<Enemy_Stats>().Enemy = new Enemy
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
            spawnedEnemies.Add(just_made.GetComponent<EnemyHealth>());
            just_made.GetComponent<EnemyHealth>().SetMaxHealth((int)stats.Enemy.Health);

        }
        AreSpawned = true;
    }
    //Simple method to spawn a boss
    //TO BE IMPROVED
    void Spawn_Boss(int id)
    {
        _boss temp = Bosses.bosses.Find(b => b.ID == id);
        GameObject just_made_boss = Instantiate(temp.Boss_Prefab, roomAssignedTo.transform);
        just_made_boss.name = temp.Name;

        just_made_boss.GetComponent<AI_Boss>().Boss = new _boss
        (
            iD: temp.ID,
            name: temp.Name,
            health: temp.Health,
            speed: temp.Speed,
            attack: temp.Attack,
            attackSpeed: temp.AttackSpeed,
            attackRange: temp.AttackRange,
            projectile: temp.Projectile,
            boss_Prefab: temp.Boss_Prefab
        );

        just_made_boss.GetComponent<BossHealth>().SetMaxHealth((int)just_made_boss.GetComponent<AI_Boss>().Boss.Health);
        GetBoss(just_made_boss);
    }
    //Splits the spawn string on ","
    int[] Split(string List)
    {

        string[] subs = List.Split(',');
        int[] temp = new int[subs.Length];
        for (int i = 0; i < subs.Length; i++)
        {
            temp[i] = int.Parse(subs[i]);
        }
        return temp;
    }

    //"how_many_to_spawn" is the number of enemies you want to spawn
    //"Range_form" and "Range_to" represent the range from the list of enemies to be randomised from.
    //Method randomises the enemy how many time you want and joins it as a string to be passed on to the spawner.
    string RandomSpawnOrder(float how_many_to_spawn, int Range_from, int Range_to)
    {
        string spawn_order = null;
        for (int i = 0; i < how_many_to_spawn; i++)
        {
            int random_enemy = Random.Range(Range_from, Range_to);
            if (i == how_many_to_spawn - 1)
            {
                spawn_order = spawn_order + random_enemy;
            }
            else if (i == 0)
            {
                spawn_order = random_enemy + ",";
            }
            else
            {
                spawn_order = spawn_order + random_enemy + ",";
            }
        }
        return spawn_order;
    }
    //Pulls all of the objects tagged as "SpawnPoint" into an array
    //Loops through it and finds a random point
    //OBSOLETE
    //Transform GetRandomSpawn(int random_int)
    //{
    //    GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

    //    for (int i = 0; i < SpawnPoints.Length; i++)
    //    {
    //        if (i == random_int)
    //        {
    //            return SpawnPoints[i].transform;
    //        }
    //    }
    //    return null;
    //}

    void GetPositions()
    {
        Vector3 roomPos = roomAssignedTo.transform.position;

        //centre
        spawnPositions.Add(roomPos);
        //middle right
        spawnPositions.Add(new Vector3(roomPos.x + 5, roomPos.y, roomPos.z));
        //middle left
        spawnPositions.Add(new Vector3(roomPos.x - 5, roomPos.y, roomPos.z));
        //top right
        spawnPositions.Add(new Vector3(roomPos.x + 5, roomPos.y, roomPos.z + 5));
        //top left
        spawnPositions.Add(new Vector3(roomPos.x - 5, roomPos.y, roomPos.z + 5));
        //top middle
        spawnPositions.Add(new Vector3(roomPos.x, roomPos.y, roomPos.z + 5));
        //bottom middle
        spawnPositions.Add(new Vector3(roomPos.x, roomPos.y, roomPos.z - 5));
        //bottom left
        spawnPositions.Add(new Vector3(roomPos.x - 5, roomPos.y, roomPos.z - 5));
        //bottom right
        spawnPositions.Add(new Vector3(roomPos.x + 5, roomPos.y, roomPos.z - 5));
    }

    Vector3 GetRandomPosition()
    {
        Vector3 pos = spawnPositions[Random.Range(0, spawnPositions.Count)];
        spawnPositions.Remove(pos);
        return pos;
    }

    public void SpawnEnemies()
    {
        GetPositions();
        Enemy_Spawn_Small(Random.Range(2, 6), 0, 2);
        GetEnemies();
    }

    public void SpawnBossEnemy()
    {
        Spawn_Boss(0);
    }
}
