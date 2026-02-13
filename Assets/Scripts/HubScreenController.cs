using UnityEngine;

public class HubScreenController : MonoBehaviour
{
    [Header("Screens (Panels)")]
    public GameObject screenMain;
    public GameObject screenExploreWorld;
    public GameObject screenStreamingHub;
    public GameObject screenMarketplace;
    public GameObject screenVirtualCampus;
    public GameObject screenNavigator;
    public GameObject screenExperiences;
    public GameObject screenMyspace;
    public GameObject screenGames;

    void Start()
    {
        ShowMain();
    }

    void HideAll()
    {
        if (screenMain != null) screenMain.SetActive(false);
        if (screenExploreWorld != null) screenExploreWorld.SetActive(false);
        if (screenStreamingHub != null) screenStreamingHub.SetActive(false);
        if (screenMarketplace != null) screenMarketplace.SetActive(false);
        if (screenVirtualCampus != null) screenVirtualCampus.SetActive(false);
        if (screenNavigator != null) screenNavigator.SetActive(false);
        if (screenExperiences != null) screenExperiences.SetActive(false);
        if (screenMyspace != null) screenMyspace.SetActive(false);
        if (screenGames != null) screenGames.SetActive(false);
    }

    public void ShowMain()
    {
        HideAll();
        if (screenMain != null) screenMain.SetActive(true);
    }

    public void ShowExploreWorld()
    {
        HideAll();
        if (screenExploreWorld != null) screenExploreWorld.SetActive(true);
    }

    public void ShowStreamingHub()
    {
        HideAll();
        if (screenStreamingHub != null) screenStreamingHub.SetActive(true);
    }

    public void Marketplace()
    {
        HideAll();
        if (screenMarketplace != null) screenMarketplace.SetActive(true);
    }

    public void ShowVirtualCampus()
    {
        HideAll();
        if (screenVirtualCampus != null) screenVirtualCampus.SetActive(true);
    }

    public void ShowNavigator()
    {
        HideAll();
        if (screenNavigator != null) screenNavigator.SetActive(true);
    }

    public void ShowExperiences()
    {
        HideAll();
        if (screenExperiences != null) screenExperiences.SetActive(true);
    }

    public void ShowMyspace()
    {
        HideAll();
        if (screenMyspace != null) screenMyspace.SetActive(true);
    }

    public void ShowGames()
    {
        HideAll();
        if (screenGames != null) screenGames.SetActive(true);
    }

    
}
