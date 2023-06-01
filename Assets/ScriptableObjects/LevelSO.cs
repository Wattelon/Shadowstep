using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int levelID;
    [SerializeField] private string sceneName;
    [SerializeField] private string levelName;
    [SerializeField] private string description;
    [SerializeField] private string goal;
    [SerializeField] private Texture preview;
    [SerializeField] private int gold;

    public int LevelID => levelID;
    public string SceneName => sceneName;
    public string LevelName => levelName;
    public string Description => description;
    public string Goal => goal;
    public Texture Preview => preview;
    public int Gold => gold;
}