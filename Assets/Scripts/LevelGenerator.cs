using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator main;

    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 8;
    [SerializeField] private Transform LevelTop;
    private int plotSize = 1;
    [SerializeField] private GameObject plotPrefab;
    [SerializeField] private GameObject pathDeleter;
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        SeaWaveManager.main.BigWave();
        StartCoroutine(DelayedGenerate());
    }

    private void GeneratePlots()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Instantiate(plotPrefab, new Vector2(LevelTop.position.x + i, LevelTop.position.y - j), Quaternion.identity);
            }
        }
    }

    private void GeneratePath()
    {

        int randomX = ((int)Random.Range(LevelTop.position.x, LevelTop.position.x + columns - 2));
        int random1X;
        do
        {
            random1X = ((int)Random.Range(LevelTop.position.x, LevelTop.position.x + columns - 2));
        } while (random1X == randomX);
        int random2X;
        do
        {
            random2X = ((int)Random.Range(LevelTop.position.x, LevelTop.position.x + columns - 2));
        } while (random2X == random1X);
        //for (int i = 0; i < points.Length; i++) {
        //    points[i].position = new Vector3(randomX + .5f, points[i].position.y, 0);
        //}
        int randomY = ((int)Random.Range(LevelTop.position.y - rows + 1, LevelTop.position.y - rows + 3));
        int random1Y = ((int)Random.Range(randomY + 2, LevelTop.position.y - rows + 5));
        points[0].position = new Vector3(randomX + .5f, points[0].position.y, 0);
        points[1].position = new Vector3(randomX + .5f, randomY + .5f, 0);
        points[2].position = new Vector3(random1X + .5f, randomY + .5f, 0);
        points[3].position = new Vector3(random1X + .5f, random1Y + .5f, 0);
        points[4].position = new Vector3(random2X + .5f, random1Y + .5f, 0);
        points[5].position = new Vector3(random2X + .5f, points[5].position.y, 0);
    }

    private void DeletePathPlots()
    {
        pathDeleter.GetComponent<PathDeleter>().DestroyPathPlots(points);
    }

    private void CleanPlots()
    {
        Plot[] allPlots = FindObjectsOfType<Plot>();
        foreach (var plot in allPlots)
        {
            Destroy(plot.gameObject);
        }
        GeneratePlots();
    }

    private void CleanTowers() {
        SandCastle[] allSandcastles = FindObjectsOfType<SandCastle>();
        foreach (var sandcastle in allSandcastles)
        {
            sandcastle.SellTower();
        }
    }
    private void CleanEffects()
    {
        DestroyAfterAnimation[] allEffects = FindObjectsOfType<DestroyAfterAnimation>();
        foreach (var effect in allEffects)
        {
            Destroy(effect.gameObject);
            
        }
    }

    IEnumerator DelayedGenerate()
    {
        yield return new WaitForSeconds(.5f);
        CleanPlots();
        CleanTowers();
        CleanEffects();
        GeneratePath();
        DeletePathPlots();
    }
}
