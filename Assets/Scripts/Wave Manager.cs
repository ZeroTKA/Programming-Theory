using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
    }
    void SpawnWave()
    {
        GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
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
