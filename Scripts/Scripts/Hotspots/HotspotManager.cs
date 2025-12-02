using UnityEngine;
using Unity.XR.CoreUtils; // XROrigin

public class HotspotManager : MonoBehaviour
{
    [SerializeField] private XROrigin xrOrigin;

    public void GoTo(Transform target)
    {
        if (xrOrigin == null || target == null) return;

        // Get the transform to move (Origin GO in rig, or the rig itself)
        Transform originXform = xrOrigin.Origin != null
            ? xrOrigin.Origin.transform
            : xrOrigin.transform;

        // Keep current Y, jump to target XZ
        Vector3 pos = originXform.position;
        pos.x = target.position.x;
        pos.z = target.position.z;
        originXform.position = pos;
    }
}
