using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI timer;

    public void WinLevel()
    {
        canvas.enabled = true;
        winScreen.SetActive(true);
        timer.text = $"На прохождение ушло {(int)Time.timeSinceLevelLoad} с";
    }

    public void LoseLevel()
    {
        canvas.enabled = true;
        loseScreen.SetActive(true);
    }

    public void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
