using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("References")]
    public ModeManager modeManager;

    [Tooltip("Drag UIRoot/PauseCanvas here (the whole canvas GameObject).")]
    public GameObject pauseCanvas;

    bool isPaused = false;

    void Start()
    {
        // Ensure pause starts closed
        isPaused = false;
        if (pauseCanvas != null) pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Only allow pausing in Bubble mode
        if (modeManager == null || !modeManager.IsBubbleMode())
        {
            // If we somehow left bubble while paused, force close.
            if (isPaused) ForceClose();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;

        if (pauseCanvas != null) pauseCanvas.SetActive(true);

        // Cursor for UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;

        if (pauseCanvas != null) pauseCanvas.SetActive(false);

        // Back to bubble controls (lock again)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public void ExitToAerial()
    {
        // Always restore time
        Time.timeScale = 1f;

        isPaused = false;
        if (pauseCanvas != null) pauseCanvas.SetActive(false);

        // Returning to map: cursor should be available for clicking
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (modeManager != null)
            modeManager.ExitToMap();
    }

    void ForceClose()
    {
        isPaused = false;
        if (pauseCanvas != null) pauseCanvas.SetActive(false);
        Time.timeScale = 1f;

        // In map mode we want cursor usable
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
