using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    private float spawnCooldown;
    private bool johnnyIsReady;
    private int randomEnemy;
    private int randomSpawnPoint;
    public int lowerSpawnTime;
    public int higherSpawnTime;

    void Start()
    {
        johnnyIsReady = false;
        spawnCooldown = Random.Range(lowerSpawnTime, higherSpawnTime);
        Invoke("heresJohnny", spawnCooldown);
    }

    void Update()
    {
        if (johnnyIsReady == true)
        {
            randomEnemy = Random.Range(0, enemyPrefabs.Length);
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation);

            johnnyIsReady = false;
        }
    }

    private void heresJohnny()
    {
        

        johnnyIsReady = true;
        spawnCooldown = Random.Range(lowerSpawnTime, higherSpawnTime);
        Invoke("heresJohnny", spawnCooldown);
    }
}
