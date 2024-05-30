using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Wave> waves = new List<Wave>();
    
    [SerializeField] private GameObject spawnerPrefab;
    private List<GameObject> spawners = new List<GameObject>();

    private float timeBetweenWaves = 60f;
    private float roundTimer = 900f;

    private int currentWaveIndex = 0;
    private float roundCountdown;

    void Start()
    {
        roundCountdown = roundTimer;

        Invoke("SpawnWave", 1f);
    }

    void Update()
    {
        roundCountdown -= Time.deltaTime;
    }

    void SpawnWave()
    {
        foreach(GameObject spawner in spawners)
        {
            Destroy(spawner);
        }
        if (currentWaveIndex >= waves.Count) return;
        
        Dictionary<EnemyType, float> currentEnemies = waves[currentWaveIndex].waveEnemies;
        foreach (EnemyType enemyType in currentEnemies.Keys)
        {
            GameObject spawner = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity);
            spawner.GetComponent<Spawner>().StartSpawning(EnemyTypeManager.Instance.GetEnemy(enemyType), currentEnemies[enemyType]);
            spawners.Add(spawner);
        }
        currentWaveIndex++;
        Invoke("SpawnWave", timeBetweenWaves);
        
        
    }
}