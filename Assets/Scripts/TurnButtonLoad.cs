using UnityEngine;
using UnityEngine.UI;

public class TurnButtonLoad : MonoBehaviour
{
    [SerializeField] private Sprite tick;
    [SerializeField] private int settingType;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Turn", 0) == settingType)
        {
            GetComponent<Image>().sprite = tick;
        }
    }
}
