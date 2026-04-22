using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    [Header("Scene Name")]
    public string mainMenuSceneName = "MainMenu";

    public void Login()
    {
        // No authentication logic for now
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ContinueAsGuest()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
    public void ExitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
}
}
