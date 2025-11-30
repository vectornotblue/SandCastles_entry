using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager main;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text bestText;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private RectTransform menuTransform;
    [SerializeField] private Image seuagullSelect;
    [SerializeField] private Image albatrosSelect;
    [SerializeField] private Image penguSelect;
    [SerializeField] private Slider eggSlider;
    [SerializeField] private bool menuOnScreen = true;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private TMP_Text seashellsText;
    private Vector2 menuPosOn;
    private Vector2 menuPosOff;
    [SerializeField] private Color nonSelectedColor = new Color(244f,210f,156f);
    [SerializeField] private Color selectedColor = new Color(129f,231f,255f);
    // private Vector2 menuPosition = new Vector2(1670, 720);
    //private Vector2 menuNoPosition = new Vector2(1670, 720);
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        // The current anchored position is the "on-screen" position
        menuPosOn = menuTransform.anchoredPosition;

        // Move it off-screen by some fixed UI offset (safe across resolutions)
        menuPosOff = menuPosOn + new Vector2(180f, 0f);  // adjust as needed
    }
    private void Update()
    {
        Vector2 target = menuOnScreen ? menuPosOn : menuPosOff;
        waveText.text = "Wave: " + EnemyManager.main.CurrentWave();
        bestText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
        menuTransform.anchoredPosition = Vector2.Lerp(
            menuTransform.anchoredPosition,
            target,
            moveSpeed * Time.deltaTime
        );
        switch (BuildManager.main.GetSelectedTowerIndex()) {
            case 0:
                seuagullSelect.color = selectedColor;
                albatrosSelect.color = nonSelectedColor;
                penguSelect.color = nonSelectedColor;
                break;
            case 1:
                seuagullSelect.color = nonSelectedColor;
                albatrosSelect.color = selectedColor;
                penguSelect.color = nonSelectedColor;
                break;
            case 2:
                seuagullSelect.color = nonSelectedColor;
                albatrosSelect.color = nonSelectedColor;
                penguSelect.color = selectedColor;
                break;
            default:
                seuagullSelect.color = nonSelectedColor;
                albatrosSelect.color = nonSelectedColor;
                penguSelect.color = nonSelectedColor;
                break;
        }
        
    }

    public void MenuExitUI()
    {
        menuOnScreen = !menuOnScreen;
    }

    public void UpdateSeashellsText(int seashells)
    {
      
        seashellsText.text = seashells.ToString();
       
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void DeselectTowers()
    {
        SandCastle[] allSandcastles = FindObjectsOfType<SandCastle>();
        foreach (var sandcastle in allSandcastles)
        {
            sandcastle.GotDeselected();
            
        }
    }


    public void EggDamage(float newEggPercent)
    {
        
        eggSlider.gameObject.GetComponent<Animator>().SetTrigger("EggLoss");
    }
}
