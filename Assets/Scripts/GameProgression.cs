using UnityEngine;

public static class GameProgression
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
}
