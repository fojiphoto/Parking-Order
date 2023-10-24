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
    public void SwitchScene() 
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.DOFillAmount(1, 5).OnComplete(() =>
        {
                SceneManager.LoadScene("MainMenu");
        });
        
    }
}
