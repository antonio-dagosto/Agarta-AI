using UnityEngine;
using UnityEngine.SceneManagement;

public class MapExitController : MonoBehaviour
{
    [Header("Scene")]
    public string startMenuSceneName = "SC_StartMenu";

    public void ExitToMainMenu()
    {
        // Optional: reset time scale just in case
        Time.timeScale = 1f;

        SceneManager.LoadScene(startMenuSceneName);
    }
}
