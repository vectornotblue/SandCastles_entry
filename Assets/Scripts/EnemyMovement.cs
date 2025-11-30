using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{



    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator enemyAnim;
    private EnemyHealth myHealth;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float defrostRate = .1f;
    private Transform target;
    private int pathIndex = 0;
    private float slowEffect = 1.0f;
    private float timeTillBurn = 0f;
    private float burnDmg = 0f;
    private void Awake()
    {
        enemyAnim.SetFloat("walkSpeed", moveSpeed);
        myHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        target = LevelManager.main.path[0];
        transform.position = LevelManager.main.startPoint.position;
    }

    private void Update()
    {
        if (burnDmg > 0f) {
            if (timeTillBurn > 0f)
            {
                timeTillBurn -= Time.deltaTime;
            }
            else
            { 
                myHealth.TakeDamage(1f);
                timeTillBurn = .2f;
                burnDmg --;
            }
        }
        if (slowEffect < 1f)
        {
            slowEffect += defrostRate * Time.deltaTime;
            if (slowEffect > 1f)
                slowEffect = 1f;
        }

        if (Vector2.Distance(target.position, transform.position) <= 0.08f){
            pathIndex++;

            
            
            if (pathIndex >= (LevelManager.main.path.Length))
            {
                EggsManager.main.EggUpdate(-1);
                FindObjectOfType<AudioManager>().Play("Boom");
                EnemyManager.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            target = LevelManager.main.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed * slowEffect;
        sr.flipX = rb.linearVelocity.x > 0 ? true : false;
    }

    public void EnemyEffect(float speedEffect)
    {
        slowEffect = speedEffect;
    }
    public float GetSlowEffect() {
        return slowEffect;
    }

    public void GetBurnt(float getburnDmg)
    {
        timeTillBurn = .2f;
        burnDmg += getburnDmg;
    }

}
