using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/LevelData")]
public class LevelData : ScriptableObject
{
    public LevelInterData[] LvlData;
}

[System.Serializable]
public class LevelInterData
{
    public int NumberofMoves;
}
