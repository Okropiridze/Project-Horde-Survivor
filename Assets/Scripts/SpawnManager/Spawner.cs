using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject enemyToSpawn;
    private float timeBetweenSpawns = 2f;
    private Transform player;
    private float spawnRadius = 20f;

    public void StartSpawning(GameObject enemy, float timeBetween)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyToSpawn = enemy;
        timeBetweenSpawns = timeBetween;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Instantiate(enemyToSpawn, GetRandomPos(), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private Vector3 GetRandomPos() {
        float randX = Random.Range(-spawnRadius, spawnRadius);
        float randY = Random.Range(-spawnRadius, spawnRadius);

        Vector3 dir = new Vector3(randX, randY, 0);
        dir.Normalize();

        return player.position + dir * spawnRadius;

    }
}
