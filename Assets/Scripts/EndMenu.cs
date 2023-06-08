using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void WinLevel()
    {
        canvas.enabled = true;
        winScreen.SetActive(true);
        _audioSource.PlayOneShot(audioWin);
        timer.text = $"На прохождение ушло {(int)Time.timeSinceLevelLoad} с";
    }

    public void LoseLevel()
    {
        canvas.enabled = true;
        loseScreen.SetActive(true);
        _audioSource.PlayOneShot(audioLose);
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