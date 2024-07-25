using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnTime = 5;
    public int Spawns = 10;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", SpawnTime, Spawns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // returns an instance of an enemy
    private GameObject SpawnEnemy()
    {
        //check if prefab is null
        if (EnemyPrefab == null)
        {
            Debug.Log("missing enemy prefab...");
            return null;
        }

        // Set random time


        // Create enemy instance
        GameObject Enemy = null;

        Debug.Log("Spawn an enemy");
        Enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        Debug.Log(Enemy);

        return Enemy;
    }
}
