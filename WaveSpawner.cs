using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // ========================================= //
    [Header("Prefabs")]

    [Tooltip("Enemy that wanted to spawn")]
    [SerializeField]
    Transform enemyPrefab;
    // ========================================= //
    [Header("Wave Properties")]

    [Tooltip("Time between each wave")]
    [SerializeField]
    float waveFrequency = 5f;
    // ========================================= //

    readonly float waitBeforeEnemySpawn = 0.225f;
    Transform startPoint;
    float countdown = 2f;
    int enemyNumber;
    int waveCount;

    public int getWaveCount { get { return waveCount; } }

    private void Awake()
    {
        waveCount = 0;
        startPoint = GameObject.FindGameObjectWithTag("Start").transform;
    }

    private void Update()
    {
        CheckCountdownTimer();
        countdown -= Time.deltaTime;
    }

    void CheckCountdownTimer()
    {
        if (countdown <= 0f)
        {
            waveCount++;
            enemyNumber = waveCount;
            StartCoroutine(SpawnWave());
            countdown = waveFrequency;
        }
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Wave " + waveCount + " Coming...");
        for (int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitBeforeEnemySpawn);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, startPoint.position, startPoint.rotation);
    }

    public float GetCountdown()
    {
        return countdown;
    }

    public int GetEnemyNumber()
    {
        return enemyNumber;
    }
}
