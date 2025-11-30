using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2;
    [SerializeField] private int currencyWorth = 10;
    [SerializeField] private Slider healthBar;
    private bool isDestroyed = false;
    private float startHitPoints;
    private void Start()
    {
        startHitPoints = hitPoints;
        UpdateHealthBar();
        
    }
    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;
        UpdateHealthBar();
        if (hitPoints <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            EnemyManager.onEnemyDestroy.Invoke();
            EnemyManagerNonProcedural.onEnemyDestroy2.Invoke();
            LevelManager.main.IncreaseSeashells(currencyWorth, transform.position);
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        float sliderAmount = (float)hitPoints / (float)startHitPoints;
        
        healthBar.SetValueWithoutNotify(sliderAmount);
    }


}
