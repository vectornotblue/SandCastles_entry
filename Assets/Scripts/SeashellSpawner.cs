using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SeashellSpawner : MonoBehaviour
{
    public static SeashellSpawner main;
    [SerializeField] Transform firstBound;
    [SerializeField] Transform secondBound;
    [SerializeField] GameObject[] seashellPrefab;
    private float waitBeforeSpawn = 3;
    [SerializeField] int[] shellWorth;
    [SerializeField] float SPS; //shells per second
    [SerializeField] private int maxSeashells = 5;
    private int totalSeashells = 0;
    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        waitBeforeSpawn -= Time.deltaTime;
        if (waitBeforeSpawn <= 0 ) { waitBeforeSpawn = 0; } 

    }

    public void SpawnSeashells()
    {
        if (totalSeashells > maxSeashells || waitBeforeSpawn > 0.1f)
        {
            return;
        }
        float xrandom = Random.Range(firstBound.position.x, secondBound.position.x);
        float yrandom = Random.Range(secondBound.position.y, firstBound.position.y);
        int indexRandom = Random.Range(0, 4);
        Instantiate(seashellPrefab[indexRandom], new Vector2(xrandom, yrandom), Quaternion.identity);
        waitBeforeSpawn += shellWorth[indexRandom] / SPS;
        totalSeashells++;
    }

    public void SeashellCollected(int seashellIndex, Vector2 seashellPosition) {
        totalSeashells--;
        LevelManager.main.IncreaseSeashells(shellWorth[seashellIndex], seashellPosition);
    }
    
}
