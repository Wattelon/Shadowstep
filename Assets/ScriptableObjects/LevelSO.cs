using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private string sceneName;
    [SerializeField] private string levelName;
    [SerializeField] private string description;
    [SerializeField] private Texture preview;
    [SerializeField] private int goal;
    
    public string SceneName => sceneName;
    public string LevelName => levelName;
    public string Description => description;
    public Texture Preview => preview;
    public int Goal => goal;
}
