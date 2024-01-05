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
    }

    private void PressPirvacy()
    {
        Application.OpenURL(PrivacyPolicy_Link);
    }

    private void PressPlay()
    {
        LoadingScene("GamePlay");
        LoadingPanel.SetActive(true);
    }

    private void LoadingScene(string Scene)
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.DOFillAmount(1,3).OnComplete(()=> 
        {
                SwitchScene(Scene);
        });
    }

    private void SwitchScene(string Scene)
    {
        //AdsManager.instance.HideMRec();
        CASAds.instance.HideMrecBanner();

        SceneManager.LoadScene(Scene);
    }
}
