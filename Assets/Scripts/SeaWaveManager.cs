using UnityEngine;

public class SeaWaveManager : MonoBehaviour
{
    public static SeaWaveManager main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform waveT;
   
    
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        
        main = this;
    }
    public void BigWave() {
        animator.SetTrigger("BigWave");

    }
    public void SendWaveData()
    {
        SeashellSpawner.main.SpawnSeashells();
    }




}
