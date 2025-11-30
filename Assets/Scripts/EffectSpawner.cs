using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject effectToSpawn;

    public void SpawnEffect()
    {
        Instantiate(effectToSpawn, transform.position, Quaternion.identity);
    }
}
