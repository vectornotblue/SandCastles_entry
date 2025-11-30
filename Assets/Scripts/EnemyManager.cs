using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject waveWarningNormal;
    [SerializeField] private GameObject waveWarningBig;
    [Header("Attributes")]

    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 2f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 1.25f;
    private int currentWave = 0;
    private float timeSinceLastSpawn;
    [SerializeField]private int enemiesAlive;
    [SerializeField]private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();


    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); 
        main = this;
    }

    private void Start()
    {

        StartCoroutine(StartWave(false));
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >=  (1f/enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            
            SpawnEnemy();
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {

        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        if (((currentWave-1) % 5 == 0) && (currentWave>1)) {
            
            StartCoroutine(StartWave(true));
            waveWarningBig.GetComponent<WaveSlider>().PlayWaveWarning(timeBetweenWaves);

        }
        else
        {
            StartCoroutine(StartWave(false));
            waveWarningNormal.GetComponent<WaveSlider>().PlayWaveWarning(timeBetweenWaves);
        }

        
        
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave(bool isBigWave)
    {
        if (currentWave > 1)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            if (isBigWave)
            {
                LevelGenerator.main.GenerateLevel();
                yield return new WaitForSeconds(1);
            }
        }
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        int randomIndex;
        if (currentWave < 10) {
            do
            {
                randomIndex = Random.Range(0, 4);
            } while (enemiesLeftToSpawn < randomIndex);
            enemiesLeftToSpawn -= randomIndex + 1;
            if (enemiesLeftToSpawn < 0)
            {
                enemiesLeftToSpawn = 0;
            }
            GameObject prefabtoSpawn = enemyPrefabs[randomIndex];
            Instantiate(prefabtoSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        } else if (currentWave < 15)
        {
            do
            {
                randomIndex = Random.Range(0, 8);
            } while (enemiesLeftToSpawn < randomIndex);
            enemiesLeftToSpawn -= randomIndex + 1;
            if (enemiesLeftToSpawn < 0)
            {
                enemiesLeftToSpawn = 0;
            }
            GameObject prefabtoSpawn = enemyPrefabs[randomIndex];
            Instantiate(prefabtoSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        } else
        {
            do
            {
                randomIndex = Random.Range(0, enemyPrefabs.Length);
            } while (enemiesLeftToSpawn < randomIndex);
            enemiesLeftToSpawn -= randomIndex + 1;
            if (enemiesLeftToSpawn < 0)
            {
                enemiesLeftToSpawn = 0;
            }
            GameObject prefabtoSpawn = enemyPrefabs[randomIndex];
            Instantiate(prefabtoSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        
    }

    private int EnemiesPerWave()
    {
        if ((currentWave - 1) % 5 == 0)
        {
            return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave-1, difficultyScalingFactor));
        }
        else {
            return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        }
        
    }
    public int CurrentWave()
    {
        return currentWave -1;
    }



}
