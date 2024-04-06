using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    private bool isCameraEnabled = true;
    private Quaternion initialRotation;

    public void ToggleCamera(bool isEnabled)
    {
        isCameraEnabled = isEnabled;
        if (!isCameraEnabled)
        {
            // Save the initial rotation when the camera is disabled
            initialRotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (!isCameraEnabled)
        {
            // Reapply the initial rotation every frame when the camera is disabled
            transform.rotation = initialRotation;
        }
    }
}
