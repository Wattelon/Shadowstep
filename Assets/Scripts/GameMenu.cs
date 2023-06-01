using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI progressTracker;
    [SerializeField] private InputActionProperty showMenu;

    private void Update()
    {
        if (showMenu.action.WasPerformedThisFrame())
        {
            canvas.enabled = !canvas.enabled;
        }
    }

    public void OnRestartButtonCLick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void TrackProgress(int progress, int goal)
    {
        progressTracker.text = $"Собрано {progress} / {goal}";
    }
}