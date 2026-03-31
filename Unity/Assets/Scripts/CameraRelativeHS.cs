using UnityEngine;

public class CameraRelativeHotspot : MonoBehaviour
{
    public float distanceInFront = 8f;
    public float verticalOffset = -1.0f;  // negative = slightly lower in view
    public bool requireRightMouseToShow = false;

    void LateUpdate()
    {
        if (Camera.main == null) return;

        // Optional: only show while right mouse is held (if you want)
        if (requireRightMouseToShow && !Input.GetMouseButton(1))
            return;

        Transform cam = Camera.main.transform;

        Vector3 targetPos =
            cam.position +
            cam.forward * distanceInFront +
            cam.up * verticalOffset;

        transform.position = targetPos;

        // Face the camera
        transform.forward = (transform.position - cam.position).normalized;
    }
}
