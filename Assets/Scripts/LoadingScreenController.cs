using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    [Header("UI (optional)")]
    public Slider progressBar;        // drag ProgressBar slider here if you made one
    public TMP_Text loadingText;      // drag a TMP text if you want status

    void Start()
    {
        StartCoroutine(LoadNextSceneAsync());
    }

    IEnumerator LoadNextSceneAsync()
    {
        string target = LoadingSceneTarget.NextSceneName;
        if (loadingText != null)
            loadingText.text = "Loading...";

        // Start async loading
        AsyncOperation op = SceneManager.LoadSceneAsync(target);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            // op.progress goes 0..0.9 until activation; normalize to 0..1
            float p = Mathf.Clamp01(op.progress / 0.9f);

            if (progressBar != null)
                progressBar.value = p;

            // When it reaches 0.9, it’s ready to activate
            if (op.progress >= 0.9f)
            {
                if (loadingText != null)
                    loadingText.text = "Almost there...";

                // small delay feels smoother (optional)
                yield return new WaitForSeconds(0.2f);

                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
