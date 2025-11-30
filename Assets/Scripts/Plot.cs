using UnityEngine;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;


    private GameObject sandCastle;
    private Color startColor;
    private bool isFull = false;


    private void Start()
    {
        startColor = sr.color;  
    }


    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (BuildManager.main.GetSelectedCastlePrice() == -1) return;
        if (isFull || sandCastle != null)
        {
            //Debug.Log("Couldn't place a sandcastle");
            return;
        }
        if (!LevelManager.main.SpendSeashells(BuildManager.main.GetSelectedCastlePrice())) return;
        
        //Debug.Log("Sandcastle placed");
        GameObject castleToBuild = BuildManager.main.GetSelectedTower();
        Instantiate(castleToBuild, transform.position, Quaternion.identity);
        BuildManager.main.SetSelectedTower(3); //clear
        isFull = true;
    }
    public void SetEmpty()
    {
        isFull = false;
    }
       
}
