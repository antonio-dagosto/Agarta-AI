using UnityEngine;

[CreateAssetMenu(menuName = "AgriMeta/Plot Data")]
public class PlotData : ScriptableObject
{
    public string plotName;

    [Header("Aerial View Target (Degrees, Meters)")]
    public double latitude;
    public double longitude;
    public double heightMeters = 3000.0;

    [Header("Enter Bubble Hotspot Location (Degrees, Meters)")]
    public double enterLatitude;
    public double enterLongitude;
    public double enterHeightMeters = 200.0;

    [Header("Enter Bubble")]
    public BubbleArea entryBubbleArea;
}
