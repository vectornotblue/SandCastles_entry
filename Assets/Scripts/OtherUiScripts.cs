using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class OtherUiScripts : MonoBehaviour
{

    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }
    public void UnFreezeGame()
    {
        Time.timeScale = 1f;
    }
    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void QuitGame()
    {
        Time.timeScale = 0f;
        Application.Quit();
    }
}
