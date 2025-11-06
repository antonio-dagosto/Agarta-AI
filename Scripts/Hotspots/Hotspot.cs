using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hotspot : MonoBehaviour
{
    [Tooltip("Where the player will be moved to (a waypoint transform).")]
    public Transform target;

    [Tooltip("Reference to the HotspotManager in the scene.")]
    public HotspotManager manager;

    // This will be called by XR Simple Interactable -> Select Entered event.
    public void OnSelected(SelectEnterEventArgs _)
    {
        if (manager != null && target != null)
        {
            manager.GoTo(target);
        }
    }
}
