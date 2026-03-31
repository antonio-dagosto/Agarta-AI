using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class HubUIController : MonoBehaviour
{
    [Header("Side Menu")]
    [Header("UI Panels")]
    public RectTransform sideBar;
    public float sideBarOpenX = 0f;
    public float sideBarClosedX = -320f;
    public float sideBarMoveSpeed = 12f;

    [Header("Menu Buttons")]
    public GameObject openMenuButton;
    public GameObject closeMenuButton;

    [Header("Settings")]
    public GameObject settingsPanel;
    public GameObject notificationsPanel;
    public GameObject favoritesPanel;


    [Header("Audio")]
    public AudioSource backgroundAudioSource;
    public Slider volumeSlider;

    [Header("Video Background")]
    public GameObject videoBackgroundObject;   // drag VideoBG here
    public VideoPlayer backgroundVideoPlayer;  // drag your VideoPlayer here

    [Header("Static Backgrounds")]
    public GameObject[] staticBackgrounds;     // drag only non-video backgrounds here
    public int currentBackgroundIndex = -1;    // -1 means video background

    [Header("Theme Colors")]
    public Image[] themedImages;
    public RawImage[] themedRawImages;
    public TMP_Text[] themedTexts;
    public Color[] backgroundThemeColors;

    bool sideBarOpen = true;
    float targetSidebarX;

    void Start()
    {
        targetSidebarX = sideBarOpen ? sideBarOpenX : sideBarClosedX;

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
            
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (notificationsPanel != null)
            notificationsPanel.SetActive(false);

        if (favoritesPanel != null)
            favoritesPanel.SetActive(false);

        UpdateMenuButtons();

        if (sideBar != null)
        {
            Vector2 pos = sideBar.anchoredPosition;
            pos.x = targetSidebarX;
            sideBar.anchoredPosition = pos;
        }

        if (volumeSlider != null)
        {
            float startVolume = 1f;

            if (backgroundAudioSource != null)
                startVolume = backgroundAudioSource.volume;

            volumeSlider.value = startVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        ApplyBackgroundState();
    }

    void Update()
    {
        if (sideBar != null)
        {
            Vector2 pos = sideBar.anchoredPosition;
            pos.x = Mathf.Lerp(pos.x, targetSidebarX, Time.unscaledDeltaTime * sideBarMoveSpeed);
            sideBar.anchoredPosition = pos;
        }
    }

    // =========================
    // SIDE MENU
    // =========================

    public void OpenSideMenu()
    {
        sideBarOpen = true;
        targetSidebarX = sideBarOpenX;
        UpdateMenuButtons();
    }

    public void CloseSideMenu()
    {
        sideBarOpen = false;
        targetSidebarX = sideBarClosedX;
        UpdateMenuButtons();
    }

    public void ToggleSideMenu()
    {
        sideBarOpen = !sideBarOpen;
        targetSidebarX = sideBarOpen ? sideBarOpenX : sideBarClosedX;
        UpdateMenuButtons();
    }

    void UpdateMenuButtons()
    {
        if (openMenuButton != null)
            openMenuButton.SetActive(!sideBarOpen);

        if (closeMenuButton != null)
            closeMenuButton.SetActive(sideBarOpen);
    }

    // =========================
    // SETTINGS
    // =========================

    public void OpenSettings()
    {
        CloseAllOverlayPanels();
        CloseSideMenu();

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Bring sidebar back when settings closes
        OpenSideMenu();
    }

    // =========================
    // VOLUME
    // =========================

    public void SetVolume(float value)
    {
        if (backgroundAudioSource != null)
            backgroundAudioSource.volume = value;
    }

    // =========================
    // BACKGROUND SWITCHING
    // =========================

    // Call this if you want to explicitly return to video background
    public void UseVideoBackground()
    {
        currentBackgroundIndex = -1;
        ApplyBackgroundState();
    }

    public void SetBackgroundByIndex(int index)
    {
        if (staticBackgrounds == null || staticBackgrounds.Length == 0)
            return;

        if (index < 0 || index >= staticBackgrounds.Length)
            return;

        currentBackgroundIndex = index;
        ApplyBackgroundState();
    }

    public void NextBackground()
{
    int staticCount = (staticBackgrounds != null) ? staticBackgrounds.Length : 0;

    if (staticCount == 0)
    {
        currentBackgroundIndex = -1;
        ApplyBackgroundState();
        return;
    }

    // Video -> first static
    if (currentBackgroundIndex == -1)
    {
        currentBackgroundIndex = 0;
    }
    else
    {
        currentBackgroundIndex++;

        // Last static -> back to video
        if (currentBackgroundIndex >= staticCount)
            currentBackgroundIndex = -1;
    }

    ApplyBackgroundState();
}

    public void PreviousBackground()
{
    int staticCount = (staticBackgrounds != null) ? staticBackgrounds.Length : 0;

    if (staticCount == 0)
    {
        currentBackgroundIndex = -1;
        ApplyBackgroundState();
        return;
    }

    // Video -> last static
    if (currentBackgroundIndex == -1)
    {
        currentBackgroundIndex = staticCount - 1;
    }
    else
    {
        currentBackgroundIndex--;

        // Before first static -> back to video
        if (currentBackgroundIndex < 0)
            currentBackgroundIndex = -1;
    }

    ApplyBackgroundState();
}

    void ApplyBackgroundState()
    {
        // VIDEO BACKGROUND ACTIVE
        bool useVideo = (currentBackgroundIndex == -1);

        if (videoBackgroundObject != null)
            videoBackgroundObject.SetActive(useVideo);

        if (backgroundVideoPlayer != null)
        {
            if (useVideo)
            {
                if (!backgroundVideoPlayer.isPlaying)
                    backgroundVideoPlayer.Play();
            }
            else
            {
                backgroundVideoPlayer.Stop();
            }
        }

        // STATIC BACKGROUNDS
        if (staticBackgrounds != null && staticBackgrounds.Length > 0)
        {
            for (int i = 0; i < staticBackgrounds.Length; i++)
            {
                if (staticBackgrounds[i] != null)
                    staticBackgrounds[i].SetActive(!useVideo && i == currentBackgroundIndex);
            }
        }

        ApplyThemeColor();
    }

    // =========================
    // COLOR THEMES
    // =========================

void ApplyThemeColor()
{
    if (backgroundThemeColors == null || backgroundThemeColors.Length == 0)
        return;

    int themeIndex;

    // Video background uses color 0
    if (currentBackgroundIndex == -1)
    {
        themeIndex = 0;
    }
    else
    {
        // Static backgrounds use color 1, 2, 3, ...
        themeIndex = currentBackgroundIndex + 1;
    }

    if (themeIndex < 0 || themeIndex >= backgroundThemeColors.Length)
        return;

    Color theme = backgroundThemeColors[themeIndex];

    if (themedImages != null)
    {
        foreach (Image img in themedImages)
        {
            if (img != null)
            {
                Color c = img.color;
                c.r = theme.r;
                c.g = theme.g;
                c.b = theme.b;
                img.color = c;
            }
        }
    }

    if (themedRawImages != null)
    {
        foreach (RawImage img in themedRawImages)
        {
            if (img != null)
            {
                Color c = img.color;
                c.r = theme.r;
                c.g = theme.g;
                c.b = theme.b;
                img.color = c;
            }
        }
    }

    if (themedTexts != null)
    {
        foreach (TMP_Text txt in themedTexts)
        {
            if (txt != null)
            {
                Color c = txt.color;
                c.r = theme.r;
                c.g = theme.g;
                c.b = theme.b;
                txt.color = c;
            }
        }
    }
}

public void OpenNotifications()
{
    CloseAllOverlayPanels();
    CloseSideMenu();

    if (notificationsPanel != null)
        notificationsPanel.SetActive(true);
}

public void CloseNotifications()
{
    if (notificationsPanel != null)
        notificationsPanel.SetActive(false);

    OpenSideMenu();
}

public void OpenFavorites()
{
    CloseAllOverlayPanels();
    CloseSideMenu();

    if (favoritesPanel != null)
        favoritesPanel.SetActive(true);
}

public void CloseFavorites()
{
    if (favoritesPanel != null)
        favoritesPanel.SetActive(false);

    OpenSideMenu();
}

void CloseAllOverlayPanels()
{
    if (settingsPanel != null)
        settingsPanel.SetActive(false);

    if (notificationsPanel != null)
        notificationsPanel.SetActive(false);

    if (favoritesPanel != null)
        favoritesPanel.SetActive(false);
}
}