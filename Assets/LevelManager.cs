using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> Levels;
    public LevelData levelData;
    public static int CurrentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.LogError(PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0)+" current Level");

        CurrentLevel = PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0);


        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(false);
        }

        if (CurrentLevel >= Levels.Count)
        {
            int C = Random.Range(5,Levels.Count);
            Levels[C].SetActive(true);
        }
        else
        {
            Levels[PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0)].SetActive(true);
        }
    }

}
