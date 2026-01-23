using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRaySelector : MonoBehaviour
{
    public float maxDistance = 1e9f;
    public LayerMask hotspotLayers = ~0;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        // Ignore UI clicks
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("MouseRaySelector: Camera.main is NULL");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hotspotLayers))
        {
            Hotspot hs = hit.collider.GetComponentInParent<Hotspot>();
            Debug.Log("Ray hit: " + hit.collider.name + " | Hotspot = " + (hs ? hs.name : "NULL"));

            if (hs != null)
                hs.Select();
        }
        else
        {
            Debug.Log("Raycast missed");
        }
    }
}
