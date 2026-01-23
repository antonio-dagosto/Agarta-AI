using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    public string loadingSceneName = "SC_Loading";
    public string tourSceneName = "SC_MapTour"; // <- set to your actual map scene name

    public void BeginTours()
    {
        // Go to loading screen, which will then load the tour scene
        SceneManager.LoadScene(loadingSceneName);
        LoadingSceneTarget.NextSceneName = tourSceneName;
    }

    public void ExitAgartaAI()
    {
        // Quits in build; stops play mode in editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
