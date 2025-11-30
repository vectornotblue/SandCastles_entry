using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
