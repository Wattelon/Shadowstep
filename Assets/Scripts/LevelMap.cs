using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private Sprite openedLevelSprite;
    [SerializeField] private Sprite closedLevelSprite;
    private int _availableLevels;

    private void OnEnable()
    {
        _availableLevels = PlayerPrefs.GetInt("Level", 0);
        _availableLevels = transform.childCount == _availableLevels ? _availableLevels : _availableLevels + 1;
        ChangeIcons(_availableLevels, true);
    }

    private void OnDisable()
    {
        ChangeIcons(transform.childCount, false);
    }

    private void ChangeIcons(int number, bool isOpened)
    {
        for (var i = 0; i < number; i++)
        {
            var button = transform.GetChild(i).GetComponent<Button>();
            button.interactable = isOpened;
            button.GetComponent<Image>().sprite = isOpened ? openedLevelSprite : closedLevelSprite;
        }
    }
}