using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CesiumForUnity;

public class PlotMenuController : MonoBehaviour
{
    [Header("UI")]
    public Transform contentParent;
    public Button plotButtonPrefab;

    [Header("Plot Data")]
    public PlotData[] plots;

    [Header("Cesium References")]
    public CesiumGlobeAnchor mapCameraRigAnchor;     // MapCameraRig's CesiumGlobeAnchor
    public CesiumGlobeAnchor plotSpawnAnchor;        // PlotSpawnAnchor's CesiumGlobeAnchor

    [Header("Enter Bubble Hotspot")]
    public EnterBubbleHotspot enterBubbleHotspot;    // HS_EnterBubble (child of PlotSpawnAnchor)

    [Header("Optional: Fix Cesium tilt after teleport")]
    [Tooltip("Drag ModeManager here if you want it to re-assert top-down after selecting a plot.")]
    public ModeManager modeManager;

    void Start()
    {
        BuildMenu();
    }

    void BuildMenu()
    {
        if (contentParent == null || plotButtonPrefab == null)
        {
            Debug.LogError("PlotMenuController: contentParent or plotButtonPrefab not assigned.");
            return;
        }

        // Clear existing buttons
        for (int i = contentParent.childCount - 1; i >= 0; i--)
            Destroy(contentParent.GetChild(i).gameObject);

        // Build one button per plot
        foreach (var plot in plots)
        {
            if (plot == null) continue;

            Button b = Instantiate(plotButtonPrefab, contentParent);

            TMP_Text t = b.GetComponentInChildren<TMP_Text>();
            if (t != null) t.text = plot.plotName;

            // Capture local variable for correct closure behavior
            PlotData capturedPlot = plot;
            b.onClick.AddListener(() => OnPlotSelected(capturedPlot));
        }
    }

    void OnPlotSelected(PlotData plot)
    {
        if (plot == null) return;

        // 1) Move the aerial camera rig (top-down view height)
        if (mapCameraRigAnchor != null)
        {
            mapCameraRigAnchor.longitudeLatitudeHeight =
                new Unity.Mathematics.double3(plot.longitude, plot.latitude, plot.heightMeters);
        }

        // 2) Move the plot spawn anchor (near where the player "spawns")
        if (plotSpawnAnchor != null)
        {
            plotSpawnAnchor.longitudeLatitudeHeight =
                new Unity.Mathematics.double3(plot.longitude, plot.latitude, plot.heightMeters);
        }

        // 3) Configure the fixed Enter Bubble hotspot with which bubble area to enter
        if (enterBubbleHotspot != null)
        {
            enterBubbleHotspot.Configure(plot.entryBubbleArea);
        }

        // 4) Optional: re-assert top-down next frame to remove Cesium-induced tilt
        if (modeManager != null)
        {
            modeManager.SnapMapToTopDown();
        }
    }
}
