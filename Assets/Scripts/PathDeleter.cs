using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PathDeleter : MonoBehaviour
{

    [SerializeField] private float step = 1f;
    public void DestroyPathPlots( Transform[] points)
    {
        transform.position = points[0].position;
        for (int i = 1; i < points.Length; i++)
        {
            while (Vector2.Distance(transform.position, points[i].position) > 0.7f)
            {
                Vector3 direction = (points[i].position - transform.position).normalized;
                transform.position += direction * step;
                Collider2D plotCollider = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Plot"));

                if (plotCollider != null)
                {
                    // Get the Plot component
                    Plot plot = plotCollider.GetComponent<Plot>();
                    if (plot != null)
                    {
                        Destroy(plot.gameObject);
                    }
                }
            }
            transform.position = points[i].position;
        }
    }
}
