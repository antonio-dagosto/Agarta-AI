using UnityEngine;
using UnityEngine.SceneManagement;  // for optional quit to menu

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    private bool isPaused = false;

    void Start()
    {
        // Make sure we start unpaused
        Time.timeScale = 1f;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("ESC detected via Input.GetKeyDown");
        TogglePause();
    }
}


    public void TogglePause()
{
    isPaused = !isPaused;
    Debug.Log("TogglePause called. isPaused = " + isPaused);

    if (pauseCanvas != null)
        pauseCanvas.SetActive(isPaused);

    Time.timeScale = isPaused ? 0f : 1f;
}

    // Button hook – Resume
    public void Resume()
    {
        if (isPaused)
            TogglePause();
    }

    // Optional: hook a button to go back to main menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
