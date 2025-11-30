using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private GameObject[] buildingPrefabs;

    private int currentSelectedCastle = 3;
    [SerializeField] private int[] towerPrice = { 50, 70, 30, -1};

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return buildingPrefabs[currentSelectedCastle];
    }


    public void SetSelectedTower(int selectedCastle)
    {
        currentSelectedCastle = selectedCastle;
    }
    
    public int GetSelectedTowerIndex()
    {
        return currentSelectedCastle;
    }

    public int GetSelectedCastlePrice()
    {
        return towerPrice[currentSelectedCastle];
    }
}
