using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "StarterScene"; // Change to your scene name

    public void StartTour()
    {
        Debug.Log("StartTour was called!");
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
