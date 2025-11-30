using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private GameObject expEffect;
    [SerializeField] private bool areaDamage = false;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float slowEffect = 1;
    [SerializeField] private float burnEffect = 0;
    [SerializeField] private float areaRadius = 1;
    private Transform target;
    private bool isActive = true;
    [SerializeField] private Vector2 initialposition;
    private void Awake()
    {
        initialposition = transform.position;
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.SetRotation(angle);
        rb.linearVelocity = direction * bulletSpeed;
        
    }

    private void Update()
    {
        if (!target) return;
        if (Vector2.Distance(initialposition, transform.position) > 10)
        {
            isActive = false;
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive) return;
        if (areaDamage)
        {
            AreaDamageHit();
        } else
        {
            SingleTargetHit(other.gameObject);
        }
    }

    private void SingleTargetHit( GameObject enemy)
    {
        if (!isActive) return;
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null) health.TakeDamage(bulletDamage);

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
        {
            if (burnEffect > 0)
            {
                movement.GetBurnt(burnEffect);
            }
            if (movement.GetSlowEffect() > slowEffect)
            {
                movement.EnemyEffect(slowEffect);
            }
            
        }
        isActive = false;
        Destroy(gameObject);
    }

    private void AreaDamageHit()
    {
        Vector2 forward = transform.right;
        Vector3 explosionPos = transform.position + (Vector3)forward*.5f;
        Instantiate(expEffect, explosionPos, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, areaRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth health = hit.GetComponent<EnemyHealth>();
                if (health != null) health.TakeDamage(bulletDamage);
                EnemyMovement movement = hit.GetComponent<EnemyMovement>();
                if (movement != null) {
                    if (burnEffect > 0)
                    {
                        movement.GetBurnt(burnEffect);
                    }
                    if (movement.GetSlowEffect() > slowEffect)
                    {
                        movement.EnemyEffect(slowEffect);
                    }
                }
                
                
            }
        }
        isActive = false;
        Destroy(gameObject);
    }
}
