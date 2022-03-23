using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject chestPrefab;
    public float spawnCooldown;
    [HideInInspector] public bool chestExists;
    private int randomSpawnPoint;
    public static ChestSpawner InstanceOfChestSpawner;

    void Start()
    {
        InstanceOfChestSpawner = this;
        chestExists = false;
        spawnCooldown = 10;
    }

    void Update()
    {
        if (chestExists == false)
        {
            Invoke("SpawnChest", spawnCooldown);
            chestExists = true;
        }
    }

    private void SpawnChest()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(chestPrefab, spawnPoints[randomSpawnPoint].position, transform.rotation);
    }
}

