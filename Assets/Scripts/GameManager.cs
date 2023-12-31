using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public int countTracks,numberOfMoves;
    public bool victorybool, losebool;
    [SerializeField] Text TotalCount_Text;
    [SerializeField] LevelData LevelData;
    [SerializeField] GameObject LevelFailPanel, LevelCompletePanel, PausePanel;

    [Header("Pause Btn")]
    [SerializeField] Button Pause_Btn;
    [Header("Complete Panel Btns")]
    [SerializeField] Button Next_Btn, Restart_Btn, Home_Btn;
    [Header("Fail Panel Btns")]
    [SerializeField] Button f_Restart_Btn, f_Home_Btn;
    [Header("Pause Panel Btns")]
    [SerializeField] Button P_Restart_Btn, P_Home_Btn,P_Resume_Btn;

    void Start()
    {
        if (gameManagerInstance == null)
            gameManagerInstance = this;

        AudioManager.instance?.SetMasterVolume(0.3f);

        countTracks = GameObject.FindGameObjectsWithTag("path").Length;

        numberOfMoves = LevelData.LvlData[LevelManager.CurrentLevel].NumberofMoves;
        UpdateMovesCount();

        Init_BtnEvents();
    }

    void Init_BtnEvents() 
    {
        Pause_Btn.onClick.AddListener(Pause);
        Next_Btn.onClick.AddListener(Next);
        Home_Btn.onClick.AddListener(Home);
        Restart_Btn.onClick.AddListener(Restart);
        
        P_Resume_Btn.onClick.AddListener(Resume);
        P_Home_Btn.onClick.AddListener(Home);
        P_Restart_Btn.onClick.AddListener(Restart);

        f_Home_Btn.onClick.AddListener(Home);
        f_Restart_Btn.onClick.AddListener(Restart);
    }

    private void Pause()
    {
        //AdsManager.instance.ShowInterstitialWithoutConditions();
        CASAds.instance.ShowInterstitial();
        AudioManager.instance?.SetMasterVolume(0);
        PausePanel.SetActive(true);
    }

    public void Restart()
    {
        AudioManager.instance?.SetMasterVolume(1);

        // AdsManager.instance.ShowInterstitialWithoutConditions();
        CASAds.instance.ShowInterstitial();
        AudioManager.instance?.Click();
        SceneManager.LoadScene("GamePlay");
        int lvl = PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0);
        if (victorybool)
            lvl--;
        LevelManager.CurrentLevel = lvl;
        PlayerPrefsManager.Set(PlayerPrefsManager.CurrentLevel, lvl);
    }

    public void Home()
    {
        AudioManager.instance?.Click();
        AudioManager.instance?.SetMasterVolume(1);

        // AdsManager.instance.ShowInterstitialWithoutConditions();
        CASAds.instance.ShowInterstitial();

        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        AudioManager.instance?.SetMasterVolume(1);
        AudioManager.instance?.Click();
        PausePanel.SetActive(false);
    }

    public void Next()
    {
        //AdsManager.instance.ShowInterstitialWithoutConditions();
        AudioManager.instance?.Click();
        SceneManager.LoadScene("GamePlay");
    }

    public void Victory()
    {
        if (countTracks.Equals(0) && !losebool)
        {
            victorybool = true;
            Debug.Log("You Win!");
            LevelCompletePanel.SetActive(true);
            int lvl = PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0);
            lvl++;
            LevelManager.CurrentLevel = lvl;
            PlayerPrefsManager.Set(PlayerPrefsManager.CurrentLevel, lvl);
            //AdsManager.instance.ShowInterstitialWithoutConditions();
            CASAds.instance.ShowInterstitial();
            AudioManager.instance?.PlaySoundEffect(AudioManager.instance?.soundEffects[1]);
        }
    }



    public void Lose()
    {
        if (losebool | numberOfMoves<=-1 && !victorybool)
        {
            Debug.Log("You Lose!");
            LevelFailPanel.SetActive(true);
            //AdsManager.instance.ShowInterstitialWithoutConditions();
            CASAds.instance.ShowInterstitial();

            AudioManager.instance?.PlaySoundEffect(AudioManager.instance?.soundEffects[2]);

        }
    }
    public void UpdateMovesCount()
    {
        if (numberOfMoves > -1)
            TotalCount_Text.text = numberOfMoves.ToString();
    }

    
}
