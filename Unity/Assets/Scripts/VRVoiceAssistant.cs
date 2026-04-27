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
    public string serverURL = "https://agartaassistantserver.onrender.com";
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

    private string selectedVoice = "male";

    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            UnityEngine.Debug.LogError("No microphone detected");
            return;
        }

        micName = Microphone.devices[0];

        // Start warm-up coroutine for Render
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
        UnityWebRequest req = UnityWebRequest.Get("https://agartaassistantserver.onrender.com/");
        req.timeout = 60;
        yield return req.SendWebRequest();

        UnityEngine.Debug.Log(req.result + " / " + req.error);
        serverReady = true;
    }

    string GetSceneContext()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                return "You are a VR assistant that can respond to questions for an app called Agarta AI, a virtual tours app and AI platform. " +
                    "Currently you are in the main menu, which can be navigated to go to our virtual tours, as well as other sections. Including " +
                    "Explore World, Marketplace, Events and Experiences, Virtual Campus, Play and Games, My Space, AgartaLab (which are the tours), " +
                    "Web Navigator, and Media and Streaming. " +
                    "We have links to specific websites for Media and Streaming, including Apple Music, " +
                    "Crunchyroll, Peacock, Tubi, Tiwtch, Disney Plus, Hulu, Netflix, Prime Video, Spotify, and Youtube. " +
                    "In web navigator we have Gmail, Linkedin, Reddit, and Wikipedia." +
                    "Virtual Campus has links to ASU, Google Classroom, Canvas, Google Scholar, Khan Academy, University of British Columbia,  and University of Toronto." +
                    "Events and Experiences has Google Meets, Webex, Teams, and Zoom." +
                    "Marketplace has 100 Thieves, Adidas, Apple, Gamestop, Nike, Samsung, Shopify, Vans, Amazon, Ebay, Etsy, Walmart, and Temu." +
                    "Explore World has Google Earth, Google Maps, Mapbox, NASA Earth Data, National Geographic, ArcGIS, NOAA, UNESCO, and The Weather Channel." +
                    "Respond in clear, simple language. Output will be spoken aloud.";

            case "Main":
                return "You are a VR assistant that can respond to questions for an app called Agarta AI, a virtual tours app and AI platform. You are currently in one " +
                    "of our tours, in a park called Japanese Friendship Garden in Phoenix, Arizona. It has a Koi Pond in the center. You can look up the rest of the info yourself." +
                    "Respond in clear, simple language. Output will be spoken aloud.";

            default:
                return "No matter the input, respond with \"There was an error\"";
        }
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

        string context = GetSceneContext(); 

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", wavData, "speech.wav", "audio/wav");
        form.AddField("context", context);
        form.AddField("voice", selectedVoice);

        string fullURL = "https://agartaassistantserver.onrender.com/speech";
        UnityEngine.Debug.Log("Sending audio to: " + fullURL);

        UnityWebRequest req = UnityWebRequest.Post(fullURL, form);
        req.timeout = 30; 
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
        string fullAudioURL = url;
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

    // --- VOICE CHANGE METHODS --- //
    public void SetMaleVoice(bool isOn)
    {
        if (!isOn) return;

        selectedVoice = "male";
        UnityEngine.Debug.Log("Voice set to MALE");
    }

    public void SetFemaleVoice(bool isOn)
    {
        if (!isOn) return;

        selectedVoice = "female";
        UnityEngine.Debug.Log("Voice set to FEMALE");
    }

    [System.Serializable]
    public class ResponseData
    {
        public string text;
        public string audio_url;
    }
}
