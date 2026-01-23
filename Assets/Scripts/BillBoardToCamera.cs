using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;
        transform.forward = (transform.position - Camera.main.transform.position).normalized;
    }
}
