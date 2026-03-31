using UnityEngine;
using UnityEngine.SceneManagement;

public class HubLogoutController : MonoBehaviour
{
    [Header("Scenes")]
    public string loginSceneName = "Login";

    public void Logout()
    {
        // Safety: if anything ever paused time
        Time.timeScale = 1f;

        SceneManager.LoadScene(loginSceneName);
    }
}
