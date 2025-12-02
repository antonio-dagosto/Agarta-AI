using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class Hotspot : MonoBehaviour
{
    [Header("Teleport")]
    public Transform target;
    public HotspotManager manager;

    [Header("Info Panel (assign in Inspector)")]
    public GameObject infoCanvas;           // world-space Canvas (child)
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Image iconImage;

    [Header("Info Content")]
    public string title;
    [TextArea(2, 4)] public string description;
    public Sprite icon;

    void Awake()
    {
        if (infoCanvas) infoCanvas.SetActive(false);
        if (titleText) titleText.text = title;
        if (bodyText)  bodyText.text  = description;
        if (iconImage) iconImage.sprite = icon;
    }

    // Called by XR Simple Interactable events
    public void OnSelected(SelectEnterEventArgs _)
    {
        if (manager != null && target != null)
            manager.GoTo(target);
    }

    public void OnHoverEntered(HoverEnterEventArgs _)
    {
        if (infoCanvas) infoCanvas.SetActive(true);
    }

    public void OnHoverExited(HoverExitEventArgs _)
    {
        if (infoCanvas) infoCanvas.SetActive(false);
    }
}
