using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static PoolManager;


public class WaveManager : MonoBehaviour
{
    

    public static WaveManager instance;
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(TheDirector.GameState.Wave == TheDirector.instance.State)
        {
            SpawnEnemiesForWave();
            // -------------------------
            // This whole section can probably be WAY faster. We spawn using OnEnable and remove with OnDisable. Just keep track of a public int. 
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
            {
                Debug.Log("moving to next wave");
                MoveToNextWave();
            }
            else if(totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 == waves.Length)
            {
                TheDirector.instance.UpdateGameState(TheDirector.GameState.Victory);
                Debug.Log("YO WIN");
            }    
            // ------------------------
        }

    }
    public void StartWaves()
    {
        // do we need to set currentWave every frame??? no way.
        Debug.Log("Start Waves called");
        currentWave = waves[currentWaveNumber];
    }
    private void MoveToNextWave()
    {
        Debug.Log("Move TO Next Wave Called");
        currentWaveNumber++;
        canSpawn = true;
        TheDirector.instance.UpdateGameState(TheDirector.GameState.Player);
    }

    void SpawnEnemiesForWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = ReturnRandomEnemy();
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
            PoolEmpty pool = FindPool(randomEnemy);
            SpawnObject(randomEnemy, randomSpawnPoint.position, Quaternion.identity, pool);
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }

        }
    }

    //The whole point of this function is to return a random enemy within our limits. Meaning, we don't want true random
    //We don't want 50 boss zombies to spawn. We want 45 small, and 5 big. This controls what we get
    GameObject ReturnRandomEnemy()
    {
        GameObject randomEnemy;
        int random = Random.Range(0, currentWave.typeOfEnemies.Length);
        if (currentWave.maxTypeOfEnemies[random] > 0)
        {
            currentWave.maxTypeOfEnemies[random]--;
            randomEnemy = currentWave.typeOfEnemies[random];
            return randomEnemy;
        }
        else
        {
            bool hasFoundEnemy = false;
            while(!hasFoundEnemy)
            {
                for (int i = 0; i < currentWave.typeOfEnemies.Length; i++)
                {
                    if (currentWave.maxTypeOfEnemies[i] > 0)
                    {
                        hasFoundEnemy = true;
                        random = i;
                        break;
                    }
                }
            }
            randomEnemy = currentWave.typeOfEnemies[random];
            return randomEnemy;
        }
               
    }
    public bool CheckIfIsLastWave()
    {
        Debug.Log("Checking Last Wave");
        Debug.Log($"{currentWaveNumber} + {waves.Length} is {currentWaveNumber == waves.Length} ");
        if (currentWaveNumber == waves.Length -1 )
        {
            Debug.Log("true");
            return true;
        }
        else
        {
            return false;
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
    public int[] maxTypeOfEnemies;
}
