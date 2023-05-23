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
    private int _goal;

    private void Start()
    {
        _goal = level.Goal;
    }

    public void GainCollectable(int price)
    {
        _progress += price;
        gameMenu.TrackProgress(_progress, _goal);
        if (_progress >= _goal)
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        postProcess.profile = winProfile;
        endMenu.WinLevel();
    }

    public void FailLevel()
    {
        Time.timeScale = 0;
        postProcess.profile = loseProfile;
        endMenu.LoseLevel();
    }
}