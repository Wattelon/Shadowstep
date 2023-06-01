using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private Sprite circle;
    private int _availableLevels;

    private void OnEnable()
    {
        _availableLevels = PlayerPrefs.GetInt("Level", 0);
        _availableLevels = transform.childCount == _availableLevels ? _availableLevels - 1 : _availableLevels;
        for (int i = 0; i <= _availableLevels; i++)
        {
            var button = transform.GetChild(i).GetComponent<Button>();
            button.interactable = true;
            button.GetComponent<Image>().sprite = circle;
        }
    }
}