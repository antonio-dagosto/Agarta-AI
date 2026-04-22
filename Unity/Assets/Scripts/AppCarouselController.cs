using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppCarouselController : MonoBehaviour
{
    [System.Serializable]
    public class AppData
    {
        public string appName;
        public Texture logo;

        [TextArea]
        public string url;
    }

    [Header("References")]
    public ScrollRect scrollRect;
    public RectTransform viewport;
    public RectTransform contentParent;

    [Header("Search UI")]
    public TMP_InputField searchInputField;
    public TextMeshProUGUI searchMessage;

    [Header("App Data")]
    public List<AppData> apps = new List<AppData>();

    [Header("Layout")]
    public Vector2 itemSize = new Vector2(220f, 270f);
    public Vector2 logoSize = new Vector2(180f, 180f);
    public float itemSpacing = 40f;

    [Header("Label")]
    public TMP_FontAsset labelFont;
    public int labelFontSize = 24;
    public Color labelColor = Color.white;

    [Header("Carousel Scaling")]
    public float minScale = 0.75f;
    public float maxScale = 1.0f;
    public float scaleDistance = 500f;

    [Header("Tap vs Drag")]
    public float dragThreshold = 15f;

    [Header("Search Animation")]
    public float scrollAnimationDuration = 0.4f;

    [Header("Center Highlight")]
    public Color centerBorderColor = Color.white;
    public float borderThickness = 6f;

    private readonly List<RectTransform> spawnedItems = new List<RectTransform>();
    private readonly List<Image> spawnedBorders = new List<Image>();
    private Coroutine scrollRoutine;

    private void Start()
    {
        ConfigureLayoutGroup();
        BuildCarousel();
        HideSearchMessage();

        if (searchInputField != null)
        {
            searchInputField.onSubmit.AddListener(delegate { SearchForApp(); });
        }
    }

    private void Update()
    {
        UpdateScaling();
    }

    [ContextMenu("Build Carousel")]
    public void BuildCarousel()
    {
        if (scrollRect == null || viewport == null || contentParent == null)
        {
            Debug.LogError("AppCarouselController is missing references.");
            return;
        }

        ClearCarousel();

        for (int i = 0; i < apps.Count; i++)
        {
            CreateAppItem(apps[i]);
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.horizontalNormalizedPosition = 0f;
        UpdateScaling();
    }

    [ContextMenu("Clear Carousel")]
    public void ClearCarousel()
    {
        spawnedItems.Clear();
        spawnedBorders.Clear();

        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(contentParent.GetChild(i).gameObject);
        }
    }

    private void ConfigureLayoutGroup()
    {
        HorizontalLayoutGroup layout = contentParent.GetComponent<HorizontalLayoutGroup>();
        if (layout != null)
        {
            layout.spacing = itemSpacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
        }
    }

    private void CreateAppItem(AppData data)
    {
        GameObject root = new GameObject(data.appName, typeof(RectTransform));
        root.transform.SetParent(contentParent, false);

        RectTransform rootRect = root.GetComponent<RectTransform>();
        rootRect.sizeDelta = itemSize;

        VerticalLayoutGroup vertical = root.AddComponent<VerticalLayoutGroup>();
        vertical.childAlignment = TextAnchor.UpperCenter;
        vertical.spacing = 12f;
        vertical.childControlWidth = false;
        vertical.childControlHeight = false;
        vertical.childForceExpandWidth = false;
        vertical.childForceExpandHeight = false;

        LayoutElement rootLayout = root.AddComponent<LayoutElement>();
        rootLayout.preferredWidth = itemSize.x;
        rootLayout.preferredHeight = itemSize.y;

        GameObject buttonObject = new GameObject(
            "LogoButton",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image),
            typeof(Button),
            typeof(CarouselAppButton)
        );

        buttonObject.transform.SetParent(root.transform, false);

        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.sizeDelta = logoSize;

        LayoutElement buttonLayout = buttonObject.AddComponent<LayoutElement>();
        buttonLayout.preferredWidth = logoSize.x;
        buttonLayout.preferredHeight = logoSize.y;

        Image buttonImage = buttonObject.GetComponent<Image>();
        buttonImage.color = new Color(1f, 1f, 1f, 0f);

        GameObject borderObject = new GameObject(
            "Border",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image)
        );

        borderObject.transform.SetParent(buttonObject.transform, false);
        borderObject.transform.SetAsFirstSibling();

        RectTransform borderRect = borderObject.GetComponent<RectTransform>();
        borderRect.anchorMin = Vector2.zero;
        borderRect.anchorMax = Vector2.one;
        borderRect.offsetMin = new Vector2(-borderThickness, -borderThickness);
        borderRect.offsetMax = new Vector2(borderThickness, borderThickness);

        Image borderImage = borderObject.GetComponent<Image>();
        borderImage.color = centerBorderColor;
        borderImage.raycastTarget = false;
        borderImage.enabled = false;

        GameObject rawObject = new GameObject(
            "Logo",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(RawImage),
            typeof(AspectRatioFitter)
        );

        rawObject.transform.SetParent(buttonObject.transform, false);
        rawObject.transform.SetAsLastSibling();

        RectTransform rawRect = rawObject.GetComponent<RectTransform>();
        rawRect.anchorMin = Vector2.zero;
        rawRect.anchorMax = Vector2.one;
        rawRect.offsetMin = Vector2.zero;
        rawRect.offsetMax = Vector2.zero;

        RawImage rawImage = rawObject.GetComponent<RawImage>();
        rawImage.texture = data.logo;
        rawImage.raycastTarget = true;

        AspectRatioFitter fitter = rawObject.GetComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;

        if (data.logo != null && data.logo.height > 0)
        {
            fitter.aspectRatio = (float)data.logo.width / data.logo.height;
        }

        GameObject labelObject = new GameObject(
            "AppName",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(TextMeshProUGUI)
        );

        labelObject.transform.SetParent(root.transform, false);

        RectTransform labelRect = labelObject.GetComponent<RectTransform>();
        labelRect.sizeDelta = new Vector2(itemSize.x, 50f);

        LayoutElement labelLayout = labelObject.AddComponent<LayoutElement>();
        labelLayout.preferredWidth = itemSize.x;
        labelLayout.preferredHeight = 50f;

        TextMeshProUGUI label = labelObject.GetComponent<TextMeshProUGUI>();
        label.text = data.appName;
        label.fontSize = labelFontSize;
        label.color = labelColor;
        label.alignment = TextAlignmentOptions.Center;
        label.enableWordWrapping = false;

        if (labelFont != null)
        {
            label.font = labelFont;
        }

        CarouselAppButton appButton = buttonObject.GetComponent<CarouselAppButton>();
        appButton.Initialize(scrollRect, data.url, dragThreshold);

        spawnedItems.Add(rootRect);
        spawnedBorders.Add(borderImage);
    }

    private void UpdateScaling()
    {
        if (viewport == null)
            return;

        Vector3 viewportCenter = viewport.TransformPoint(viewport.rect.center);

        int closestIndex = -1;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < spawnedItems.Count; i++)
        {
            RectTransform item = spawnedItems[i];
            if (item == null)
                continue;

            Vector3 itemCenter = item.TransformPoint(item.rect.center);
            float distance = Mathf.Abs(itemCenter.x - viewportCenter.x);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }

            float t = Mathf.Clamp01(distance / scaleDistance);
            float scale = Mathf.Lerp(maxScale, minScale, t);

            item.localScale = new Vector3(scale, scale, 1f);
        }

        for (int i = 0; i < spawnedBorders.Count; i++)
        {
            if (spawnedBorders[i] != null)
            {
                spawnedBorders[i].enabled = (i == closestIndex);
            }
        }
    }

    public void SearchForApp()
    {
        if (searchInputField == null)
            return;

        string query = searchInputField.text.Trim();

        if (string.IsNullOrEmpty(query))
        {
            ShowSearchMessage("Enter an app name.");
            return;
        }

        int foundIndex = -1;

        for (int i = 0; i < apps.Count; i++)
        {
            if (apps[i].appName.ToLower().Contains(query.ToLower()))
            {
                foundIndex = i;
                break;
            }
        }

        if (foundIndex >= 0)
        {
            HideSearchMessage();
            ScrollToIndex(foundIndex);
        }
        else
        {
            ShowSearchMessage("App not found on this page.");
        }
    }

    public void ScrollToIndex(int index)
    {
        if (index < 0 || index >= spawnedItems.Count)
            return;

        Canvas.ForceUpdateCanvases();

        float target = GetNormalizedPositionForItem(index);

        if (scrollRoutine != null)
        {
            StopCoroutine(scrollRoutine);
        }

        scrollRoutine = StartCoroutine(AnimateScroll(target));
    }

    private float GetNormalizedPositionForItem(int index)
    {
        if (spawnedItems.Count <= 1)
            return 0f;

        float contentWidth = contentParent.rect.width;
        float viewportWidth = viewport.rect.width;
        float scrollableWidth = contentWidth - viewportWidth;

        if (scrollableWidth <= 0f)
            return 0f;

        RectTransform item = spawnedItems[index];
        Vector2 itemLocalPos = (Vector2)contentParent.InverseTransformPoint(item.TransformPoint(item.rect.center));

        float desiredContentX = itemLocalPos.x - (viewportWidth * 0.5f);
        float normalized = Mathf.Clamp01(desiredContentX / scrollableWidth);

        return normalized;
    }

    private IEnumerator AnimateScroll(float target)
    {
        float start = scrollRect.horizontalNormalizedPosition;
        float time = 0f;

        while (time < scrollAnimationDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / scrollAnimationDuration);
            t = Mathf.SmoothStep(0f, 1f, t);

            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(start, target, t);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }

    private void ShowSearchMessage(string message)
    {
        if (searchMessage != null)
        {
            searchMessage.gameObject.SetActive(true);
            searchMessage.text = message;
        }
    }

    private void HideSearchMessage()
    {
        if (searchMessage != null)
        {
            searchMessage.text = "";
            searchMessage.gameObject.SetActive(false);
        }
    }
}