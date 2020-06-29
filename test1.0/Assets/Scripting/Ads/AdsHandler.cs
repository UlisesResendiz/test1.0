using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsHandler : MonoBehaviour, IUnityAdsListener
{
    

    private string a_IDAppStore = "3644700";
    private string a_IDPlayStore = "3644701";
    private string a_InterstitialAd = "video";

    public bool a_isTargetPlayStore;
    public bool a_isTestAdd;

    bool a_ShowAdd;
    const float a_TimerforAdDisplay = 30;
    static float a_TimerAd = 0;
    static bool a_AddBeingDisplayed;

    string a_SceneDestination = "";
    
    private void Start()
    {
        SetUp();      
    }
    
    void SetUp()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
    }

    private void Update()
    {
        AdTimer();
    }

    void InitializeAdvertisement()
    {
        if (a_isTargetPlayStore)
        {
            Advertisement.Initialize(a_IDPlayStore, a_isTestAdd);
            return;
        }

        Advertisement.Initialize(a_IDAppStore, a_isTestAdd);
    }

    public void PlayInterstitialAd(string Destination)
    {
        if (a_TimerAd <= 0)
        {
            if (!Advertisement.IsReady(a_InterstitialAd))
            {
                return;
            }
            Time.timeScale = 0;
            a_SceneDestination = Destination;
            Advertisement.Show(a_InterstitialAd);
        }
        else
        {
            a_SceneDestination = Destination;
            OnUnityAdsDidFinish("", ShowResult.Finished);
        }
    }

    public static void AdTimer()
    {
        if (a_TimerAd > 0)
        {
            a_TimerAd -= Time.deltaTime;
            Debug.Log(a_TimerAd);
        }
    }

    public static bool ShowAdd()
    {
        if (a_TimerAd <= 0)
        {
            return true;
        }

        return false;
    }

    public void OnUnityAdsReady(string placementId)
    {
     //   throw new NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        // throw new NotImplementedException();
        a_AddBeingDisplayed = false;
        a_TimerAd = a_TimerforAdDisplay;
        Time.timeScale = 1;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // throw new NotImplementedException();
        a_TimerAd = a_TimerforAdDisplay;
        Time.timeScale = 0;
    }

    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Advertisement.RemoveListener(this);
        StartCoroutine(AsyncSceneLoad());
    }
    

    IEnumerator AsyncSceneLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(a_SceneDestination);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public bool isAdBeingDisplayed()
    {
        return a_AddBeingDisplayed;
    }

    
}
