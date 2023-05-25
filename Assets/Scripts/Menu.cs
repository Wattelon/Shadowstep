using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private RawImage preview;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI levelDescription;
    [SerializeField] private GameObject start;
    
    private string _sceneName;

    public void OnQuitButtonClick()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void OnResetButtonClick()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void OnMasterValueChange(float value)
    {
        masterMixer.SetFloat("MasterVolume", value);
    }

    public void OnMusicValueChange(float value)
    {
        masterMixer.SetFloat("MusicVolume", value);
    }

    public void OnSoundValueChange(float value)
    {
        masterMixer.SetFloat("SoundVolume", value);
    }

    public void OnLevelIconClick(LevelSO level)
    {
        preview.texture = level.Preview;
        levelName.text = level.LevelName;
        levelDescription.text = level.Description;
        _sceneName = level.SceneName;
        start.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
