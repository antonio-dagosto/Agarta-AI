using UnityEngine;
using UnityEngine.SceneManagement;

public class HubMenuController : MonoBehaviour
{
    [Header("Scenes")]
    public string agartaLabScene = "AgartaLabCreation";

    [Header("Tours Flow")]
    public bool useLoadingScreen = true;
    public string loadingScene = "Loading";
    public string toursScene = "Main";

    [Header("Auth/Exit")]
    public string loginScene = "Login";

    public void OpenAgartaLab() => SceneManager.LoadScene(agartaLabScene);


    public void OpenAgartaTours()
    {
        if (useLoadingScreen)
        {
            LoadingSceneTarget.TargetScene = toursScene;
            SceneManager.LoadScene(loadingScene);
        }
        else
        {
            SceneManager.LoadScene(toursScene);
        }
    }

    public void Logout()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(loginScene);
    }
}
