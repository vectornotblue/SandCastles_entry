using UnityEngine;

public class DuckPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int pickBlink;
    [SerializeField] private Animator ducksAnimator;
    public void PickBlink()
    {
        pickBlink = Random.Range(-2, 4);
        if (pickBlink < 0) { pickBlink = 0; }
        ducksAnimator.SetInteger("randomAnim", pickBlink);
    }

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music2");
    }
}
