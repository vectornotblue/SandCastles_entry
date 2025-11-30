using UnityEngine;

public class KeepSize : MonoBehaviour
{
    // Desired width and height in pixels
    public int desiredWidth = 1920;
    public int desiredHeight = 1080;

    void Start()
    {
        SetFixedViewportSize();
    }

    void SetFixedViewportSize()
    {
        // Calculate the aspect ratio
        float targetAspect = (float)desiredWidth / (float)desiredHeight;

        // Get the current screen aspect ratio
        float screenAspect = (float)Screen.width / (float)Screen.height;

        // Determine the scaling factor to maintain the desired aspect ratio
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = Camera.main;

        // Adjust the camera's orthographic size or field of view
        if (camera.orthographic)
        {
            // Orthographic camera: adjust the orthographic size
            if (scaleHeight < 1.0f)
            {
                camera.orthographicSize = desiredHeight / 2.0f;
            }
            else
            {
                camera.orthographicSize = desiredHeight / (2.0f * scaleHeight);
            }
        }
        else
        {
            // Perspective camera: adjust field of view
            if (scaleHeight < 1.0f)
            {
                camera.fieldOfView = desiredHeight / 2.0f;
            }
            else
            {
                camera.fieldOfView = desiredHeight / (scaleHeight);
            }
        }

        // Set the aspect ratio to the target aspect ratio
        Screen.SetResolution(desiredWidth, desiredHeight, true);
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure viewport size is maintained during runtime
        SetFixedViewportSize();
    }
}
