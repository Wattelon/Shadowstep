using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] private LevelSO level;
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private EndMenu endMenu;
    [SerializeField] private PostProcessVolume postProcess;
    [SerializeField] private PostProcessProfile winProfile;
    [SerializeField] private PostProcessProfile loseProfile;

    private int _progress;
    private int _gold;

    private void Start()
    {
        _gold = level.Gold;
        gameMenu.ShowGoal(level.Goal);
    }

    public void GainCollectable(int price)
    {
        _progress += price;
        gameMenu.TrackProgress(_progress, _gold);
        if (_progress >= _gold)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        postProcess.profile = winProfile;
        endMenu.WinLevel();
        Progression.CompleteLevel(level.LevelID);
    }

    public void FailLevel()
    {
        Time.timeScale = 0;
        postProcess.profile = loseProfile;
        endMenu.LoseLevel();
    }
}