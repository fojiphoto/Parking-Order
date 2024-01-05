using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    [SerializeField] Image LoadingBar;
    // Start is called before the first frame update
    void Start() 
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.DOFillAmount(1, 12).OnComplete(() =>
        {
            //AdsManager.instance.ShowBanner();
                SceneManager.LoadScene("MainMenu");
                 CASAds.instance.ShowBanner(CAS.AdPosition.BottomCenter);
        });
        
    }
}
