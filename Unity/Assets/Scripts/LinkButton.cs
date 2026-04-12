using UnityEngine;
using UnityEngine.UI;

public class LogoLinkItem : MonoBehaviour
{
    public RawImage logoImage;
    private string websiteURL;

    public void Setup(Texture logoTexture, string url)
    {
        if (logoImage != null)
            logoImage.texture = logoTexture;

        websiteURL = url;
    }

    public void OpenWebsite()
    {
        if (!string.IsNullOrWhiteSpace(websiteURL))
        {
            Application.OpenURL(websiteURL);
        }
        else
        {
            Debug.LogWarning("No URL assigned for " + gameObject.name);
        }
    }
}