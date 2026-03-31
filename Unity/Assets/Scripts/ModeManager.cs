using UnityEngine;
using CesiumForUnity;
using System.Collections;

public class ModeManager : MonoBehaviour
{
    public enum AppMode { Map, Bubble }

    [Header("Mode Roots")]
    public GameObject mapModeRoot;
    public GameObject bubbleModeRoot;

    [Header("UI Roots")]
    public GameObject uiRoot;
    public GameObject bubbleCanvas;
    public GameObject pauseCanvas;

    [Header("Bubble")]
    public BubbleController bubbleController;

    [Header("Map Camera / Cesium")]
    public CesiumGlobeAnchor mapCameraRigAnchor;   // MapCameraRig's CesiumGlobeAnchor
    public Transform mapCameraRigTransform;        // MapCameraRig Transform
    public Transform mapCameraTransform;           // MapCamera Transform (child of rig)

    [Header("Fixed Exit-To-Aerial Location")]
    public double fixedExitLatitude = 39.736401;
    public double fixedExitLongitude = -105.25737;
    public double fixedExitHeight = 2250.0;

    AppMode currentMode = AppMode.Map;

    void Awake()
    {
        if (uiRoot != null) uiRoot.SetActive(true);
        SetMode(AppMode.Map);
    }

    public bool IsBubbleMode() => currentMode == AppMode.Bubble;

    public void EnterBubble(BubbleArea startArea)
    {
        SetMode(AppMode.Bubble);

        if (bubbleController != null && startArea != null)
            bubbleController.LoadArea(startArea);
    }

    public void ExitToMap()
    {
        if (pauseCanvas != null) pauseCanvas.SetActive(false);

        SetMode(AppMode.Map);

        // Snap to your fixed aerial location, then enforce top-down pose
        if (mapCameraRigAnchor != null)
        {
            mapCameraRigAnchor.longitudeLatitudeHeight =
                new Unity.Mathematics.double3(fixedExitLongitude, fixedExitLatitude, fixedExitHeight);
        }

        SnapMapToTopDown();
    }

    public void SetMode(AppMode mode)
    {
        currentMode = mode;

        if (mapModeRoot != null) mapModeRoot.SetActive(mode == AppMode.Map);
        if (bubbleModeRoot != null) bubbleModeRoot.SetActive(mode == AppMode.Bubble);

        if (bubbleCanvas != null) bubbleCanvas.SetActive(mode == AppMode.Bubble);
        if (pauseCanvas != null) pauseCanvas.SetActive(false);
    }

    // Call this after ANY Cesium teleport to remove accumulated tilt
    public void SnapMapToTopDown()
    {
        StopAllCoroutines();
        StartCoroutine(SnapMapToTopDownNextFrame());
    }

    IEnumerator SnapMapToTopDownNextFrame()
    {
        // Wait for Cesium to finish applying the anchor transform this frame
        yield return null;

        // Reset rig rotation (kills tilt that can get introduced on teleport)
        if (mapCameraRigTransform != null)
            mapCameraRigTransform.rotation = Quaternion.identity;

        // Force camera to look straight down (local so it’s stable)
        if (mapCameraTransform != null)
            mapCameraTransform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
