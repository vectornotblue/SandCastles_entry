using UnityEngine;

public class CurencyShell : MonoBehaviour
{
    [SerializeField] private float[] rndLenghtRange = {-.05f, .05f};
    [SerializeField] private float[] rndPosRange = {-.1f, .1f} ;
    [SerializeField] private float baseLenght = .4f;
    public Transform targetPosition;
    private float elapsedTime;
    private Vector3 startPos;
    private void Awake()
    {
        
        baseLenght += Random.Range(rndLenghtRange[0], rndLenghtRange[1]);
        startPos = new Vector3(transform.position.x + Random.Range(rndPosRange[0], rndPosRange[1]), transform.position.y + Random.Range(rndPosRange[0], rndPosRange[1]), 0);
        elapsedTime = 0.0f;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPosition.position) <= 0.1f)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.Lerp(startPos, targetPosition.position, elapsedTime/baseLenght);
    }
}
