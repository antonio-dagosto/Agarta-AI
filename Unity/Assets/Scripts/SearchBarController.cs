using UnityEngine;
using TMPro;

public class SearchBarController : MonoBehaviour
{
    public TMP_InputField inputField;

    public void Search()
    {
        if (inputField == null) return;

        string query = inputField.text;

        if (string.IsNullOrEmpty(query))
            return;

        // Replace spaces with + for URL
        string formatted = query.Replace(" ", "+");

        string url = "https://www.google.com/search?q=" + formatted;

        Application.OpenURL(url);

        inputField.text = "";
    }

    // Optional: press Enter to search
    public void OnSubmit(string value)
    {
        Search();
    }


// This is for when the website is set up, change
    public void ContactUs(string value)
    {
        //Application.OpenURL();
    }

}