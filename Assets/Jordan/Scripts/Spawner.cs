using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnCooldown = 1;
    private float timeUntilSpawn = 0;
    GameObject prefabToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Spawn();

            timeUntilSpawn = spawnCooldown;
        }

    }
    private void Spawn()
    {
        Vector3 newPos = new Vector3(Random.Range(gameObject.transform.position.x, gameObject.transform.position.x+2), Random.Range(gameObject.transform.position.y, gameObject.transform.position.y+2), 0);
        prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject spawn = Instantiate(prefabToSpawn, newPos, Quaternion.identity) as GameObject;
        spawn.GetComponent<Rigidbody>().velocity = gameObject.transform.GetComponent<Rigidbody>().velocity;
    }
}
