using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    private int _availableLevels;

    private void OnEnable()
    {
        _availableLevels = PlayerPrefs.GetInt("Level", 0);
        _availableLevels = transform.childCount == _availableLevels ? _availableLevels - 1 : _availableLevels;
        for (int i = 0; i <= _availableLevels; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
}
