using UnityEngine;
using TMPro;
using System;

public class DateTimeDisplay : MonoBehaviour
{
    public TMP_Text dateTimeText;

    [Tooltip("How often to refresh the display, in seconds.")]
    public float updateInterval = 1f;

    float timer;

    void Start()
    {
        UpdateDateTime();
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            UpdateDateTime();
        }
    }

    void UpdateDateTime()
    {
        DateTime now = DateTime.Now;

        // Example format:
        // MONDAY
        // APRIL 1, 2026
        // 11:42 AM
        dateTimeText.text =
            now.ToString("MMMM d, yyyy").ToUpper()  + " " +
            now.ToString("dddd").ToUpper() + " " +
            now.ToString("h:mm tt");
    }
}
