using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSlider : MonoBehaviour
{
    [SerializeField]  private Animator warningAnimator;
    

    public void PlayWaveWarning(float slideTime)
    {
        warningAnimator.SetFloat("slideTime", 1 / slideTime);
        warningAnimator.SetTrigger("Warning");
    }

    
}
