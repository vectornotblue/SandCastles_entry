using UnityEngine;

public class IcePuddle : MonoBehaviour
{
    [SerializeField] private float slowEffect = .4f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other == null) return;
        EnemyMovement movement = other.gameObject.GetComponent<EnemyMovement>();
        if (movement != null) movement.EnemyEffect(slowEffect);
    }
}
