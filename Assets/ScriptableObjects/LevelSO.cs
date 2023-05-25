using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int levelID;
    [SerializeField] private string sceneName;
    [SerializeField] private string levelName;
    [SerializeField] private string description;
    [SerializeField] private Texture preview;
    [SerializeField] private int goal;

    public int LevelID => levelID;
    public string SceneName => sceneName;
    public string LevelName => levelName;
    public string Description => description;
    public Texture Preview => preview;
    public int Goal => goal;
}
