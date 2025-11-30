using UnityEngine;
using UnityEngine.UI;

public class AudioUiManager : MonoBehaviour
{
    public float audioLvl;
    
    public void MuteAudio()
    {
        audioLvl = 0;
    }

    public void UnMuteAudio()
    {
        audioLvl = 1;
    }

}
