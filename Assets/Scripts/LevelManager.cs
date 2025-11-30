using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    [SerializeField] private Transform CurrencyShellTarget;
    [SerializeField] private GameObject currencyShell;
    public int seashells;
    [SerializeField] private int startSeashells = 100;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        seashells = 0;
        IncreaseSeashells(startSeashells, new Vector3(100f, 100f, 0f));
        //UIManager.main.UpdateSeashellsText(seashells);
    }

    public void IncreaseSeashells(int amount, Vector2 position)
    {
        seashells += amount;
        UIManager.main.UpdateSeashellsText(seashells);
        int totalVisualsShells = Mathf.FloorToInt(amount/5);
        for (int i = 0; i <= totalVisualsShells; i++)
        {
            GameObject shell = Instantiate(currencyShell, position, Quaternion.identity);
            shell.GetComponent<CurencyShell>().targetPosition = CurrencyShellTarget;

        }
    }

    public bool SpendSeashells(int amount)
    {
        if(amount <= seashells)
        {
            //buy
            seashells -= amount;
            UIManager.main.UpdateSeashellsText(seashells);
            return true;
        }else
        {
            //Debug.Log("NO SEASHELLS");
            return false;
        }
    }

    
}
