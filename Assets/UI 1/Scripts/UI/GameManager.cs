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
    //[SerializeField] LevelData LevelData;
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

        countTracks = GameObject.FindGameObjectsWithTag("path").Length;

       // numberOfMoves = LevelData.LvlData[LevelManager.CurrentLevel].NumberofMoves;
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
        PausePanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
        int lvl = PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0);
        if (victorybool)
            lvl--;
        LevelManager.CurrentLevel = lvl;
        PlayerPrefsManager.Set(PlayerPrefsManager.CurrentLevel, lvl);
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
    }

    public void Next()
    {
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
        }
    }



    public void Lose()
    {
        if (losebool | numberOfMoves<=-1 && !victorybool)
        {
            Debug.Log("You Lose!");
            LevelFailPanel.SetActive(true);
        }
    }
    public void UpdateMovesCount()
    {
        if (numberOfMoves > -1)
            TotalCount_Text.text = numberOfMoves.ToString();
    }

    
}
