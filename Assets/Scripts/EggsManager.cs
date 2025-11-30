using UnityEngine;
using UnityEngine.Rendering;

public class EggsManager : MonoBehaviour
{
    public static EggsManager main;
    
    [SerializeField] private int eggCount = 10;
    [SerializeField] private GameObject LoseScreen;
    
    private int currentEggCount;
    private void Awake()
    {
        main = this;
        currentEggCount = eggCount;

    }

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");
    }
    public void EggUpdate(int eggChange)
    {
        currentEggCount += eggChange;
        float eggPercentage = ((float)currentEggCount) / ((float)eggCount);
        Debug.Log(currentEggCount);
        if (currentEggCount < 0)
        {
            LoseFunction();
        }
        UIManager.main.EggDamage(eggPercentage);

    }

    public float GetEggPercentage()
    {
        return ((float)currentEggCount) / ((float)eggCount);
    }

    private void LoseFunction()
    {
        int currentWave = EnemyManager.main.CurrentWave();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentWave > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentWave);
            PlayerPrefs.Save();
        }
        LoseScreen.SetActive(true);
    }
}
