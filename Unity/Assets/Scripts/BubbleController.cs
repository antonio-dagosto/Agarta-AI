using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    [Header("Sphere")]
    public MeshRenderer sphereRenderer;     // Inverted sphere MeshRenderer
    public Transform sphereTransform;       // Inverted sphere Transform (hotspots parent here)

    [Header("Hotspot Prefab")]
    public GameObject bubbleHotspotPrefab;

    [Tooltip("How close to the wall (0.98 = 98% of radius).")]
    public float wallFactor = 0.50f;

    [Header("Icon Placement")]
    [Tooltip("How far to pull the ICON inward from the wall (fraction of sphere radius).")]
    public float iconInsetFraction = 0.15f; // helps avoid clipping & brings icons closer

    [Header("Navigation Icons")]
    public Sprite nextIcon;
    public Sprite prevIcon;

    [Header("UI Root (Bubble Mode)")]
    [Tooltip("Drag UIRoot/BubbleCanvas here so we can force-enable it when showing info.")]
    public GameObject bubbleCanvasRoot;

    [Header("Info Panel UI")]
    public GameObject infoPanel;
    public TMP_Text infoTitleText;
    public TMP_Text infoBodyText;
    public Button infoCloseButton;

    BubbleArea current;

    void Start()
    {
        if (infoCloseButton != null)
            infoCloseButton.onClick.AddListener(CloseInfo);

        CloseInfo();
    }

    // ============================================================
    // PUBLIC API
    // ============================================================

    public void LoadArea(BubbleArea area)
    {
        Debug.Log("BubbleController.LoadArea: " + (area ? area.name : "NULL"));

        current = area;

        // Swap the 360 texture
        if (sphereRenderer != null && area != null && area.equirect360 != null)
            sphereRenderer.material.mainTexture = area.equirect360;

        ClearHotspots();

        if (area != null)
        {
            SpawnAreaHotspots(area);
            SpawnNavigationHotspots(area);
        }

        CloseInfo();
    }

    // ============================================================
    // HOTSPOT SPAWNING
    // ============================================================

    void SpawnAreaHotspots(BubbleArea area)
    {
        if (area.hotspots == null) return;

        foreach (var h in area.hotspots)
        {
            SpawnHotspot(
                h.title,
                h.body,
                h.icon,
                BubbleHotspotInstance.Kind.Info,
                h.yaw,
                h.pitch
            );
        }
    }

    void SpawnNavigationHotspots(BubbleArea area)
    {
        if (area.nextArea != null && nextIcon != null)
        {
            SpawnHotspot(
                "Next Area",
                "Go to the next area",
                nextIcon,
                BubbleHotspotInstance.Kind.Next,
                60f,
                -10f
            );
        }

        if (area.prevArea != null && prevIcon != null)
        {
            SpawnHotspot(
                "Previous Area",
                "Go to the previous area",
                prevIcon,
                BubbleHotspotInstance.Kind.Prev,
                -60f,
                -10f
            );
        }
    }

    void SpawnHotspot(
        string title,
        string body,
        Sprite icon,
        BubbleHotspotInstance.Kind kind,
        float yawDeg,
        float pitchDeg
    )
    {
        if (bubbleHotspotPrefab == null || sphereTransform == null)
        {
            Debug.LogWarning("BubbleController: Missing bubbleHotspotPrefab or sphereTransform.");
            return;
        }

        GameObject go = Instantiate(bubbleHotspotPrefab, sphereTransform);
        go.name = $"HS_{kind}_{Sanitize(title)}";

        Vector3 dir = YawPitchToDirection(yawDeg, pitchDeg);

        float wallRadius = GetSphereRadiusLocal() * wallFactor;

        // Place root (collider) in local space
        go.transform.localPosition = dir * wallRadius;

        // Face toward center (nice for debugging / consistent transforms)
        go.transform.localRotation = Quaternion.LookRotation(-dir, Vector3.up);

        // Instance data
        var inst = go.GetComponent<BubbleHotspotInstance>();
        if (inst == null) inst = go.AddComponent<BubbleHotspotInstance>();

        inst.title = title;
        inst.body = body;
        inst.kind = kind;

        if (inst.hotspot == null)
            inst.hotspot = go.GetComponent<Hotspot>();

        // ICON SETUP (NO SCALING HERE — prefab controls size)
        Transform iconChild = go.transform.Find("Icon");
        if (iconChild != null)
        {
            SpriteRenderer sr = iconChild.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sprite = icon;

            // Pull icon inward so it doesn't clip and feels closer
            float inset = GetSphereRadiusLocal() * iconInsetFraction;
            iconChild.localPosition = (-dir) * inset;

            // IMPORTANT: DO NOT SCALE HERE.
            // Size the icon via prefab (Icon child scale) and/or sprite import (PPU).
        }
        else
        {
            // If you put SpriteRenderer on the root instead of an Icon child, support that too:
            SpriteRenderer rootSR = go.GetComponent<SpriteRenderer>();
            if (rootSR != null) rootSR.sprite = icon;
        }

        // Click handling
        if (inst.hotspot != null)
        {
            inst.hotspot.onSelect.RemoveAllListeners();
            inst.hotspot.onSelect.AddListener(() => OnHotspotSelected(inst));
        }
    }

    // ============================================================
    // INTERACTION
    // ============================================================

    void OnHotspotSelected(BubbleHotspotInstance inst)
    {
        if (inst == null) return;

        Debug.Log("Clicked hotspot: " + inst.kind + " | " + inst.title);

        switch (inst.kind)
        {
            case BubbleHotspotInstance.Kind.Info:
                OpenInfo(inst.title, inst.body);
                break;

            case BubbleHotspotInstance.Kind.Next:
                if (current != null && current.nextArea != null)
                    LoadArea(current.nextArea);
                break;

            case BubbleHotspotInstance.Kind.Prev:
                if (current != null && current.prevArea != null)
                    LoadArea(current.prevArea);
                break;
        }
    }

    // ============================================================
    // UI
    // ============================================================

    void OpenInfo(string title, string body)
    {
        // Force-enable BubbleCanvas (parent) so InfoPanel can actually appear
        if (bubbleCanvasRoot != null && !bubbleCanvasRoot.activeSelf)
            bubbleCanvasRoot.SetActive(true);

        if (infoPanel == null) return;

        infoPanel.SetActive(true);

        if (infoTitleText) infoTitleText.text = title;
        if (infoBodyText) infoBodyText.text = body;
    }

    void CloseInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    // ============================================================
    // UTILITIES
    // ============================================================

    float GetSphereRadiusLocal()
    {
        if (sphereTransform == null) return 0.5f;

        MeshFilter mf = sphereTransform.GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
            return mf.sharedMesh.bounds.extents.x;

        return 0.5f; // fallback for Unity sphere
    }

    Vector3 YawPitchToDirection(float yawDeg, float pitchDeg)
    {
        float yaw = yawDeg * Mathf.Deg2Rad;
        float pitch = pitchDeg * Mathf.Deg2Rad;

        return new Vector3(
            Mathf.Sin(yaw) * Mathf.Cos(pitch),
            Mathf.Sin(pitch),
            Mathf.Cos(yaw) * Mathf.Cos(pitch)
        ).normalized;
    }

    void ClearHotspots()
    {
        if (sphereTransform == null) return;

        for (int i = sphereTransform.childCount - 1; i >= 0; i--)
        {
            Transform c = sphereTransform.GetChild(i);
            if (c.GetComponent<BubbleHotspotInstance>() != null)
                Destroy(c.gameObject);
        }
    }

    static string Sanitize(string s)
    {
        if (string.IsNullOrEmpty(s)) return "Untitled";
        return s.Replace(" ", "_");
    }
}
