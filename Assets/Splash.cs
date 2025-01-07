using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public static Splash instance;
    [SerializeField] Image LoadingBar;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SwitchScene() 
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.DOFillAmount(1, 5).OnComplete(() =>
        {
                AdsManager.instance.ShowBanner();
                SceneManager.LoadScene("MainMenu");
                 //CASAds.instance.ShowBanner(CAS.AdPosition.BottomCenter);
        });
        
    }
}
