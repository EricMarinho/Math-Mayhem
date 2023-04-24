using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombSpawnManager : MonoBehaviour
{
    [SerializeField] private DisarmTheBombData disarmTheBombData;
    private List<Transform> spawnPositions;
    private int randomSpawnPosition;
    private PoolSpawner poolSpawner;
    private float timerToSpawn = 0f;
    private float timeToSpawn;

    private void Start()
    {
        poolSpawner = GetComponent<PoolSpawner>();
        spawnPositions = new List<Transform>();
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child);
        }
        timerToSpawn = disarmTheBombData.bombSpawnTime.minValue;
    }

    private void Update()
    {
        timerToSpawn += Time.deltaTime;
        if (timerToSpawn >= timeToSpawn)
        {
            timerToSpawn = 0f;
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        timeToSpawn = Random.Range(disarmTheBombData.bombSpawnTime.minValue, disarmTheBombData.bombSpawnTime.maxValue);
        randomSpawnPosition = Random.Range(0, spawnPositions.Count);
        GameObject spawnedBomb = poolSpawner.SpawnFromPool("Bomb", spawnPositions[randomSpawnPosition].position, Quaternion.identity);
        spawnedBomb.GetComponent<BombController>().poolSpawner = poolSpawner;
    }
}
