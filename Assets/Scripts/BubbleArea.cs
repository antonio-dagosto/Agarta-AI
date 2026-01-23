using UnityEngine;

[CreateAssetMenu(menuName = "AgriMeta/Bubble Area")]
public class BubbleArea : ScriptableObject
{
    public string areaName;

    [Header("360 Image")]
    public Texture2D equirect360;

    [Header("UI Text")]
    [TextArea] public string description;

    [Header("Navigation")]
    public BubbleArea nextArea;
    public BubbleArea prevArea;

    [Header("Hotspots in this area")]
    public BubbleHotspotData[] hotspots;
}

[System.Serializable]
public class BubbleHotspotData
{
    public string title;
    [TextArea] public string body;

    [Header("Placement")]
    public float yaw;
    public float pitch;

    [Header("Icon")]
    public Sprite icon;
}
