using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    public void OnQuitButtonClick()
    {
        Application.Quit();
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

    public void OnLevelIconClick(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
