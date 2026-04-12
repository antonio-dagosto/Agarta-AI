using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLinkGridController : MonoBehaviour
{
    [System.Serializable]
    public class LinkItem
    {
        public string displayName;
        public Texture logo;
        [TextArea]
        public string url;
    }

    [Header("Grid Parent")]
    public Transform gridParent;

    [Header("Button Settings")]
    public Vector2 buttonSize = new Vector2(220f, 220f);

    [Header("Optional Debug")]
    public bool showDebugBackground = false;

    [Header("Links")]
    public List<LinkItem> links = new List<LinkItem>();

    void Start()
    {
        BuildGrid();
    }

    [ContextMenu("Build Grid")]
    public void BuildGrid()
    {
        if (gridParent == null)
        {
            Debug.LogError("Grid Parent is not assigned on " + gameObject.name);
            return;
        }

        ClearGrid();

        for (int i = 0; i < links.Count; i++)
        {
            CreateLogoButton(links[i]);
        }

        RectTransform gridRect = gridParent as RectTransform;
        if (gridRect != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(gridRect);
        }
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        if (gridParent == null)
            return;

        for (int i = gridParent.childCount - 1; i >= 0; i--)
        {
            GameObject child = gridParent.GetChild(i).gameObject;

            if (Application.isPlaying)
                Destroy(child);
            else
                DestroyImmediate(child);
        }
    }

    void CreateLogoButton(LinkItem item)
    {
        GameObject buttonObj = new GameObject(
            string.IsNullOrWhiteSpace(item.displayName) ? "LogoButton" : item.displayName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image),
            typeof(Button),
            typeof(LayoutElement)
        );

        buttonObj.transform.SetParent(gridParent, false);

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.localScale = Vector3.one;
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.pivot = new Vector2(0.5f, 0.5f);
        buttonRect.sizeDelta = buttonSize;
        buttonRect.anchoredPosition = Vector2.zero;

        LayoutElement layout = buttonObj.GetComponent<LayoutElement>();
        layout.preferredWidth = buttonSize.x;
        layout.preferredHeight = buttonSize.y;
        layout.minWidth = buttonSize.x;
        layout.minHeight = buttonSize.y;

        Image bg = buttonObj.GetComponent<Image>();
        bg.raycastTarget = true;

        if (showDebugBackground)
            bg.color = new Color(1f, 0f, 0f, 0.25f);
        else
            bg.color = new Color(1f, 1f, 1f, 0f);

        Button button = buttonObj.GetComponent<Button>();
        string savedURL = item.url;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OpenURL(savedURL));

        GameObject logoObj = new GameObject(
            "Logo",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(RawImage)
        );

        logoObj.transform.SetParent(buttonObj.transform, false);

        RectTransform logoRect = logoObj.GetComponent<RectTransform>();
        logoRect.localScale = Vector3.one;
        logoRect.anchorMin = Vector2.zero;
        logoRect.anchorMax = Vector2.one;
        logoRect.pivot = new Vector2(0.5f, 0.5f);
        logoRect.offsetMin = Vector2.zero;
        logoRect.offsetMax = Vector2.zero;
        logoRect.anchoredPosition = Vector2.zero;

        RawImage raw = logoObj.GetComponent<RawImage>();
        raw.texture = item.logo;
        raw.color = Color.white;
        raw.raycastTarget = true;

        if (item.logo == null)
        {
            Debug.LogWarning("Missing logo for: " + item.displayName, buttonObj);
        }
        else
        {
            Debug.Log("Assigned logo for: " + item.displayName, buttonObj);
        }
    }

    void OpenURL(string url)
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("Empty URL on " + gameObject.name);
        }
    }
}