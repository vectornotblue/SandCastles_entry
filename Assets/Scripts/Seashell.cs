using UnityEngine;

public class Seashell : MonoBehaviour
{
    private bool isActive = true;
    [SerializeField] private int seashellIndex;

    private void Awake()
    {
        transform.Rotate(0f, 0f, Random.Range(-20f,20f));
    }
    private void OnMouseDown()
    {
        if (!isActive) return;
        isActive = false;
        SeashellSpawner.main.SeashellCollected(seashellIndex, transform.position);
        Destroy(gameObject);
    }
}
