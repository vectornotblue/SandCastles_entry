using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public int enemyCount;          // Number of enemies to spawn
    public float spawnDelay;        // Time between spawning each enemy in the wave
}

public class EnemyManagerNonProcedural : MonoBehaviour
{
    public Transform startPoint;                 // Start point to spawn enemies at
    public EnemyWave[] waves;                    // Array of waves
    public float timeBetweenWaves = 5f;          // Time between each wave (in seconds)
    private int currentWaveIndex = 0;            // Tracks the current wave being spawned
    private int enemiesAlive = 0;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy2 = new UnityEvent();
    private void Awake()
    {
        onEnemyDestroy2.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        // Start the first wave when the game starts
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        // Ensure we don't try to spawn a wave when all waves are completed
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        // Get the current wave to spawn
        EnemyWave currentWave = waves[currentWaveIndex];

        // Spawn enemies for the current wave
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            // Wait for the specified delay before spawning the next enemy
            yield return new WaitForSeconds(currentWave.spawnDelay);
        }
        
        // Wait for the time between waves before spawning the next wave
        
        currentWaveIndex++;

            // Continue spawning the next wave
        StartCoroutine(SpawnWave());
        
        
        
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (startPoint != null && enemyPrefab != null)
        {
            // Instantiate the enemy at the start point with no rotation
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity);
            enemiesAlive++;
        }
        else
        {
            Debug.LogWarning("Start point or enemy prefab is missing!");
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
}
