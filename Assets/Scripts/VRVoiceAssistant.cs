using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.XR;
using System;
using System.Diagnostics;
using System.Net.Sockets;

public class VRVoiceAssistant : MonoBehaviour
{
    [Header("Server")]
    public string serverURL = "https://aiserverpy.onrender.com/";
    private bool serverReady = false;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Input")]
    public KeyCode keyboardKey = KeyCode.Space;
    public XRNode vrHand = XRNode.RightHand;
    public InputFeatureUsage<bool> vrButton = CommonUsages.triggerButton;

    private AudioClip recording;
    private string micName;
    private bool isRecording;

    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            UnityEngine.Debug.LogError("No microphone detected");
            return;
        }

        micName = Microphone.devices[0];

        // Start warm-up coroutine for Render
        //StartCoroutine(WarmUpServer());
        StartCoroutine(TestRequest());

        try
        {
            using TcpClient client = new TcpClient("aiserverpy.onrender.com", 443);
            UnityEngine.Debug.Log("TCP connection succeeded!");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("TCP connection failed: " + e.Message);
        }
    }

    IEnumerator TestRequest()
    {
        UnityWebRequest req = UnityWebRequest.Get("https://aiserverpy.onrender.com/");
        req.timeout = 60;
        yield return req.SendWebRequest();

        UnityEngine.Debug.Log(req.result + " / " + req.error);
        serverReady = true;
    }

    public void StartRecording()
    {
        if (!serverReady)
        {
            UnityEngine.Debug.LogWarning("Cannot start recording: Server not ready yet");
            return;
        }

        if (isRecording) return;

        isRecording = true;
        recording = Microphone.Start(micName, false, 10, 44100);
        UnityEngine.Debug.Log("Recording started");
    }

    public void StopRecording()
    {
        if (!isRecording) return;

        isRecording = false;
        Microphone.End(micName);

        if (recording == null)
        {
            UnityEngine.Debug.LogError("Recording is null");
            return;
        }

        UnityEngine.Debug.Log("Recording stopped");
        StartCoroutine(SendAudio());
    }

    IEnumerator SendAudio()
    {
        if (recording == null) yield break;

        byte[] wavData = WavUtility.FromAudioClip(recording);

        if (wavData == null || wavData.Length == 0)
        {
            UnityEngine.Debug.LogError("Invalid WAV data");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", wavData, "speech.wav", "audio/wav");

        string fullURL = "https://aiserverpy.onrender.com/speech";
        UnityEngine.Debug.Log("Sending audio to: " + fullURL);

        UnityWebRequest req = UnityWebRequest.Post(fullURL, form);
        req.timeout = 30; // 30 second timeout
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Upload failed: " + req.error);
            yield break;
        }

        ResponseData json = JsonUtility.FromJson<ResponseData>(req.downloadHandler.text);
        UnityEngine.Debug.Log("Received audio URL: " + json.audio_url);

        StartCoroutine(PlayAudio(json.audio_url));
    }

    IEnumerator PlayAudio(string url)
    {
        string fullAudioURL = serverURL + url;
        UnityEngine.Debug.Log("Downloading audio from: " + fullAudioURL);

        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(fullAudioURL, AudioType.MPEG);
        req.timeout = 30;
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Audio download failed: " + req.error);
            yield break;
        }

        audioSource.clip = DownloadHandlerAudioClip.GetContent(req);
        audioSource.spatialBlend = 1f;
        audioSource.Play();
    }

    void Update()
    {
        if (!serverReady) return;

        // Keyboard
        if (Input.GetKeyDown(keyboardKey)) StartRecording();
        if (Input.GetKeyUp(keyboardKey)) StopRecording();

        // VR Controller
        InputDevice device = InputDevices.GetDeviceAtXRNode(vrHand);
        if (device.TryGetFeatureValue(vrButton, out bool pressed))
        {
            if (pressed && !isRecording) StartRecording();
            else if (!pressed && isRecording) StopRecording();
        }
    }

    [System.Serializable]
    public class ResponseData
    {
        public string text;
        public string audio_url;
    }
}
