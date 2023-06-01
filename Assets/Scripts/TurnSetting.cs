using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnSetting : MonoBehaviour
{
    private ActionBasedContinuousTurnProvider _continuousTurnProvider;
    private ActionBasedSnapTurnProvider _snapTurnProvider;

    private void Awake()
    {
        _continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
        _snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
    }

    private void Start()
    {
        UpdateSetting();
    }

    public void UpdateSetting()
    {
        var setting = PlayerPrefs.GetInt("Turn", 0);
        if (setting == 1)
        {
            _snapTurnProvider.enabled = true;
            _continuousTurnProvider.enabled = false;
        }
        else
        {
            _continuousTurnProvider.enabled = true;
            _snapTurnProvider.enabled = false;
        }
    }
}
