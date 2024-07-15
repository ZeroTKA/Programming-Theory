using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static PoolManager;


public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;

    private void Update()
    {
        // do we need to set currentWave every frame??? no way.
        currentWave = waves[currentWaveNumber];
        SpawnEnemiesForWave();

        // -------------------------
        // This whole section can probably be WAY faster. We spawn using OnEnable and remove with OnDisable. Just keep track of a public int. 
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber+1 != waves.Length)
        {
            MoveToNextWave();
        }

        // ------------------------
    }
    private void MoveToNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnEnemiesForWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
            Debug.Log($"WE are here with this random enemy named {randomEnemy.name}");
            PoolEmpty pool = FindPool(randomEnemy);
            SpawnObject(randomEnemy, randomSpawnPoint.position, Quaternion.identity,pool);
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }

        }
    }
}

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
