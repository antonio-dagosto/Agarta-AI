using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OpenExploreWorld()
    {
        SceneManager.LoadScene("ExploreWorld");
    }

    public void OpenStreamingHub()
    {
        SceneManager.LoadScene("StreamingHub");
    }

    public void OpenVirtualCampus()
    {
        SceneManager.LoadScene("VirtualCampus");
    }

    public void OpenAgartaLab()
    {
        SceneManager.LoadScene("AgartaLab");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
