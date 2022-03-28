using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner InstanceOfEnemySpawner;
    public Transform Portal1;
    public Transform Portal2;
    public Transform Portal3;
    public Transform Portal4;
    public Transform Portal5;
    public Transform Portal6;
    public int Level;
    public Transform nextSpawnPoint;
    public GameObject[] enemyPrefabs;
    private float spawnCooldown;
    private bool johnnyIsReady;
    private int randomEnemy;
    private int randomSpawnPoint;
    private int chooseNextSpawnPoint;

    private void Awake()
    {
        InstanceOfEnemySpawner = this;
    }

    void Start()
    {
        johnnyIsReady = false;
        Level = 1;
        spawnCooldown = 4;
        ChooseNextSpawnPoint();
        Invoke("heresJohnny", spawnCooldown);
        Invoke("changeSpawnCooldownAndLevel", 30f);
    }

    void Update()
    {
        if (johnnyIsReady == true)
        {
            randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], nextSpawnPoint.position, transform.rotation);
            johnnyIsReady = false;
            ChooseNextSpawnPoint();
        }
    }

    private void ChooseNextSpawnPoint()
    {
        chooseNextSpawnPoint = Random.Range(1, 6);
        if(chooseNextSpawnPoint == 1)
        {
            nextSpawnPoint = Portal1;
        }
        else if (chooseNextSpawnPoint == 2)
        {
            nextSpawnPoint = Portal2;
        }
        else if (chooseNextSpawnPoint == 3)
        {
            nextSpawnPoint = Portal3;
        }
        else if (chooseNextSpawnPoint == 4)
        {
            nextSpawnPoint = Portal4;
        }
        else if (chooseNextSpawnPoint == 5)
        {
            nextSpawnPoint = Portal5;
        }
        else if (chooseNextSpawnPoint == 6)
        {
            nextSpawnPoint = Portal6;
        }
    }

    private void heresJohnny()
    {
        johnnyIsReady = true;
        Invoke("heresJohnny", spawnCooldown);
    }

    private void changeSpawnCooldownAndLevel()
    {
        if (spawnCooldown > 1.3)
        {
            Level++;
            spawnCooldown = spawnCooldown - 0.3f;
            Invoke("changeSpawnCooldownAndLevel", 30f);
        }
    }

}
