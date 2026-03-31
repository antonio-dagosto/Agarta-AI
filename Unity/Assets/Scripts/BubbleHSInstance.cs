using UnityEngine;

public class BubbleHotspotInstance : MonoBehaviour
{
    public Hotspot hotspot;

    [HideInInspector] public string title;
    [HideInInspector] public string body;

    public enum Kind { Info, Next, Prev }
    [HideInInspector] public Kind kind;

    void Reset()
    {
        hotspot = GetComponent<Hotspot>();
    }
}
