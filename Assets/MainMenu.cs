using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button PrivacyPolicy;
    [SerializeField] string PrivacyPolicy_Link;
    [SerializeField] Image LoadingBar;
    [SerializeField] private GameObject LoadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        LoadingPanel.SetActive(false);
        Play.onClick.AddListener(PressPlay);
        PrivacyPolicy.onClick.AddListener(PressPirvacy);

        AudioManager.instance?.PlayBackgroundMusic();
    }

    private void PressPirvacy()
    {
        AudioManager.instance.Click();

        Application.OpenURL(PrivacyPolicy_Link);
    }

    private void PressPlay()
    {
        LoadingScene("GamePlay");
        LoadingPanel.SetActive(true);
        AudioManager.instance.Click();
    }

    private void LoadingScene(string Scene)
    {
        LoadingBar.fillAmount = 0;
        Invoke(nameof(ShowAd),4);
        LoadingBar.DOFillAmount(1,8).OnComplete(()=> 
        {
                SwitchScene(Scene);
        });
    }

    void ShowAd() 
    {
        CASAds.instance?.ShowInterstitial();
    }
    private void SwitchScene(string Scene)
    {
        //AdsManager.instance.HideMRec();
        CASAds.instance.HideMrecBanner();

        SceneManager.LoadScene(Scene);
    }

    public void RevokeConcent()
    {
        CASAds.instance?.HideBanner();
        CASAds.instance?.HideMrecBanner();
        PlayerPrefs.SetInt("GDPR", 0);
        Application.LoadLevel("GDPR");
    }
}
