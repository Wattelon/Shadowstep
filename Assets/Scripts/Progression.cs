using UnityEngine;

public static class Progression
{
    private static int _levelsCompleted = PlayerPrefs.GetInt("Level", 0);

    public static void CompleteLevel(int levelID)
    {
        if (levelID > _levelsCompleted)
        {
            _levelsCompleted++;
            PlayerPrefs.SetInt("Level", _levelsCompleted);
        }
    }

    public static void SaveEquipment(int equipmentID)
    {
        PlayerPrefs.SetInt($"{equipmentID}", 1);
    }
    
    public static void SaveSetting(bool settings)
    {
        PlayerPrefs.SetInt("Turn", settings ? 1 : 0);
    }
}