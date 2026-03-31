using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    public Transform lookTarget;

    void LateUpdate()
    {
        if (lookTarget == null) return;
        transform.LookAt(lookTarget);
    }
}
