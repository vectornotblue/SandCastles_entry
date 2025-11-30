using UnityEngine;
using TMPro;

public class SandCastle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject[] bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer effectSr;
    [SerializeField] private Transform rangeSr;
    [SerializeField] private Sprite[] towerlvlSprites;
    [SerializeField] private Sprite[] effectlvlSprites;

    [Header("Attributes")]
    [SerializeField] private float[] targetingRange = { 5f, 5.5f, 5.8f, 6f };
    [SerializeField] private float[] bps = { 1f, 1.35f, 1.6f, 1.8f };
    [SerializeField] private Animator _animator;
    [SerializeField] private int[] upgradePrice = { 20, 30, 40 };
    [SerializeField] private int[] sellPrice = { 40, 50, 60, 70 };
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private TMP_Text sellText;
    [SerializeField] private RectTransform upgradeButton;
    [SerializeField] private RectTransform sellButton;

    [SerializeField] private int current_lvl = 0;

    private Transform target;
    private float timeSinceLastShot = 1f;
    private bool isSelected = true;

    private void Start()
    {
        UpdateTowerVisuals();
        SelectedVisuals();
    }

    private void Update()
    {
        if (target == null || !TargetStillValid())
        {
            AcquireNewTarget();
            return;
        }

        RotateTowardsTarget();

        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= (1f / bps[current_lvl]))
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private bool TargetStillValid()
    {
        if (target == null) return false;

        if (!target.gameObject.activeInHierarchy) return false;

        float dist = Vector2.Distance(target.position, transform.position);
        return dist <= targetingRange[current_lvl];
    }

    private void AcquireNewTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            targetingRange[current_lvl],
            enemyMask
        );

        if (hits.Length == 0)
        {
            target = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        Transform best = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(hit.transform.position, transform.position);

            if (dist < closestDist)
            {
                closestDist = dist;
                best = hit.transform;
            }
        }

        target = best;
    }


    private void Shoot()
    {
        _animator.SetTrigger("Shoot");
        FindObjectOfType<AudioManager>().Play("Shoot");
        GameObject bulletObj = Instantiate(
            bulletPrefab[current_lvl],
            firingPoint.position,
            Quaternion.identity
        );

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        bool right = (target.position.x - transform.position.x) > 0;

        sr.flipX = right;
        effectSr.flipX = right;

        float offset = right ? 0.2f : -0.2f;
        firingPoint.position = new Vector2(transform.position.x + offset, transform.position.y);
    }

    public void UpgradeTowerLvl()
    {
        if (current_lvl >= 3) return;
        if (!LevelManager.main.SpendSeashells(upgradePrice[current_lvl])) return;

        current_lvl++;
        UpdateTowerVisuals();
    }

    public void LowerTowerLvls(int lvls)
    {
        current_lvl -= lvls;
        UpdateTowerVisuals();
    }

    public void UpdateTowerVisuals()
    {
        rangeSr.localScale = Vector3.one * targetingRange[current_lvl] * 2;
        sr.sprite = towerlvlSprites[current_lvl];
        effectSr.sprite = effectlvlSprites[current_lvl];

        if (current_lvl >= 3)
        {
            upgradeText.text = "max";
        }
        else
        {
            upgradeText.text = "UPGRADE: " + upgradePrice[current_lvl];
        }

        sellText.text = "SELL: " + sellPrice[current_lvl];
    }

    public void SellTower()
    {
        LevelManager.main.IncreaseSeashells(sellPrice[current_lvl]*2, transform.position);

        Collider2D plotCollider = Physics2D.OverlapCircle(
            transform.position,
            0.1f,
            LayerMask.GetMask("Plot")
        );

        if (plotCollider != null)
        {
            Plot plot = plotCollider.GetComponent<Plot>();
            if (plot != null) plot.SetEmpty();
        }

        Destroy(gameObject);
    }

 
    public void SelectedVisuals()
    {
        isSelected = !isSelected;

        rangeSr.gameObject.SetActive(isSelected);
        effectSr.gameObject.SetActive(isSelected);
        upgradeButton.gameObject.SetActive(isSelected);
        sellButton.gameObject.SetActive(isSelected);

        if (!isSelected) return;

        SandCastle[] all = FindObjectsOfType<SandCastle>();
        foreach (var s in all)
        {
            if (s != this) s.GotDeselected();
        }
    }

    public void GotDeselected()
    {
        isSelected = true;
        SelectedVisuals();
    }
}
