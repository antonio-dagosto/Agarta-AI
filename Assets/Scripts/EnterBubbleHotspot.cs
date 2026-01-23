using UnityEngine;

public class EnterBubbleHotspot : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Drag your ModeManager here")]
    public ModeManager modeManager;

    [Header("Current Entry Area (set by PlotMenuController)")]
    public BubbleArea entryArea;

    Hotspot hotspot;

    void Awake()
    {
        hotspot = GetComponent<Hotspot>();

        if (hotspot != null)
        {
            // Ensure only one listener so we don't stack listeners across play sessions
            hotspot.onSelect.RemoveAllListeners();
            hotspot.onSelect.AddListener(Enter);
        }
        else
        {
            Debug.LogWarning("EnterBubbleHotspot: No Hotspot component found on " + name);
        }
    }

    // This matches your PlotMenuController call: Configure(plot.entryBubbleArea)
    public void Configure(BubbleArea area)
    {
        entryArea = area;
    }

    public void Enter()
    {
        if (modeManager == null)
        {
            Debug.LogWarning("EnterBubbleHotspot.Enter: modeManager is NULL (assign it in Inspector).");
            return;
        }

        if (entryArea == null)
        {
            Debug.LogWarning("EnterBubbleHotspot.Enter: entryArea is NULL (select a plot first).");
            return;
        }

        modeManager.EnterBubble(entryArea);
    }
}
