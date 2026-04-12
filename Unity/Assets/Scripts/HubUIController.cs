using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class HubUIController : MonoBehaviour
{
    const string BackgroundPrefKey = "HubBackgroundIndex";
    const string VolumePrefKey = "HubVolume";

    [Header("Side Menu")]
    public RectTransform sideBar;
    public float sideBarOpenX = 0f;
    public float sideBarClosedX = -320f;
    public float sideBarMoveSpeed = 12f;

    [Header("Menu Buttons")]
    public GameObject openMenuButton;
    public GameObject closeMenuButton;

    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject notificationsPanel;
    public GameObject favoritesPanel;

    [Header("Audio")]
    public AudioSource backgroundAudioSource;
    public Slider volumeSlider;

    [Header("Video Background")]
    [Tooltip("The RawImage/GameObject that displays the video RenderTexture.")]
    public GameObject videoBackgroundObject;

    [Tooltip("The GameObject that has the VideoPlayer component on it.")]
    public VideoPlayer backgroundVideoPlayer;

    [Header("Static Backgrounds")]
    [Tooltip("Only drag NON-video background GameObjects here.")]
    public GameObject[] staticBackgrounds;

    [Tooltip("-1 = video background, 0+ = static background index")]
    public int currentBackgroundIndex = -1;

    [Header("Theme Colors")]
    [Tooltip("UI Image components that should change color with the selected background theme.")]
    public Image[] themedImages;

    [Tooltip("UI RawImage components that should change color with the selected background theme.")]
    public RawImage[] themedRawImages;

    [Tooltip("TMP texts that should change color with the selected background theme.")]
    public TMP_Text[] themedTexts;

    [Tooltip("Element 0 = video theme, Element 1 = static background 0 theme, Element 2 = static background 1 theme, etc.")]
    public Color[] backgroundThemeColors;

    bool sideBarOpen = true;
    float targetSidebarX;

    void Start()
    {
        // Load saved background selection (-1 means video)
        currentBackgroundIndex = PlayerPrefs.GetInt(BackgroundPrefKey, -1);

        targetSidebarX = sideBarOpen ? sideBarOpenX : sideBarClosedX;

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
            float startVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);

            if (backgroundAudioSource != null)
                backgroundAudioSource.volume = startVolume;

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
    // PANELS
    // =========================

    void CloseAllOverlayPanels()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (notificationsPanel != null)
            notificationsPanel.SetActive(false);

        if (favoritesPanel != null)
            favoritesPanel.SetActive(false);
    }

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

        OpenSideMenu();
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

    // =========================
    // VOLUME
    // =========================

    public void SetVolume(float value)
    {
        if (backgroundAudioSource != null)
            backgroundAudioSource.volume = value;

        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    // =========================
    // BACKGROUND SWITCHING
    // =========================

    public void UseVideoBackground()
    {
        currentBackgroundIndex = -1;
        SaveBackgroundPreference();
        ApplyBackgroundState();
    }

    public void SetBackgroundByIndex(int index)
    {
        if (staticBackgrounds == null || staticBackgrounds.Length == 0)
            return;

        if (index < 0 || index >= staticBackgrounds.Length)
            return;

        currentBackgroundIndex = index;
        SaveBackgroundPreference();
        ApplyBackgroundState();
    }

    public void NextBackground()
    {
        int staticCount = (staticBackgrounds != null) ? staticBackgrounds.Length : 0;

        if (staticCount == 0)
        {
            currentBackgroundIndex = -1;
            SaveBackgroundPreference();
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

        SaveBackgroundPreference();
        ApplyBackgroundState();
    }

    public void PreviousBackground()
    {
        int staticCount = (staticBackgrounds != null) ? staticBackgrounds.Length : 0;

        if (staticCount == 0)
        {
            currentBackgroundIndex = -1;
            SaveBackgroundPreference();
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

        SaveBackgroundPreference();
        ApplyBackgroundState();
    }

    void SaveBackgroundPreference()
    {
        PlayerPrefs.SetInt(BackgroundPrefKey, currentBackgroundIndex);
        PlayerPrefs.Save();
    }

    void ApplyBackgroundState()
    {
        bool useVideo = (currentBackgroundIndex == -1);

        // Show / hide the UI object that displays the video
        if (videoBackgroundObject != null)
            videoBackgroundObject.SetActive(useVideo);

        // Play / stop the video itself
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

        // Show only the selected static background
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

        // Video background uses theme color 0
        if (currentBackgroundIndex == -1)
        {
            themeIndex = 0;
        }
        else
        {
            // Static background 0 uses color 1, static background 1 uses color 2, etc.
            themeIndex = currentBackgroundIndex + 1;
        }

        if (themeIndex < 0 || themeIndex >= backgroundThemeColors.Length)
            return;

        Color theme = backgroundThemeColors[themeIndex];

        // Image components: preserve alpha, replace RGB only
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

        // RawImage components: preserve alpha, replace RGB only
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

        // Text components: preserve alpha, replace RGB only
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

    // =========================
    // OPTIONAL RESET
    // =========================

    public void ResetHubPreferences()
    {
        PlayerPrefs.DeleteKey(BackgroundPrefKey);
        PlayerPrefs.DeleteKey(VolumePrefKey);
        PlayerPrefs.Save();

        currentBackgroundIndex = -1;

        if (backgroundAudioSource != null)
            backgroundAudioSource.volume = 1f;

        if (volumeSlider != null)
            volumeSlider.value = 1f;

        ApplyBackgroundState();
    }
}