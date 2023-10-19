using UnityEngine;
using UnityEngine.UI;

public class GDPR : MonoBehaviour
{
    [SerializeField] Button Accept;
    [SerializeField] Splash Splash;
    private void Awake()
    {
        if (PlayerPrefsManager.Get("GDPR", 0) > 0)
        {
            Time.timeScale = 1;
            Splash.SwitchScene();
            this.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            Accept.onClick.AddListener(GDPRAccept);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void GDPRAccept()
    {
        Time.timeScale = 1;
        PlayerPrefsManager.Set("GDPR", 1);
        Splash.SwitchScene();
        this.gameObject.SetActive(false);
    }
}
