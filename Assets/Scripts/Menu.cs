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
    [SerializeField] private TextMeshProUGUI levelGoal;
    [SerializeField] private GameObject start;
    [SerializeField] private TurnSetting turnSetting;

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
        levelGoal.text = $"Цель - {level.Goal}";
        _sceneName = level.SceneName;
        start.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void TurnSetting(bool setting)
    {
        Progression.SaveSetting(setting);
        turnSetting.UpdateSetting();
    }
}