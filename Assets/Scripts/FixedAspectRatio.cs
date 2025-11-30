using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspect = 4f / 3f;

    void Start()
    {
        // Apply once on start
        ApplyAspect();
    }

    void Update()
    {
        // Re-apply if the user resizes the window
        if (Mathf.Abs((float)Screen.width / Screen.height - targetAspect) > 0.01f)
            ApplyAspect();
    }

    void ApplyAspect()
    {
        int width = Screen.width;
        int height = Mathf.RoundToInt(width / targetAspect);
        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }
}
