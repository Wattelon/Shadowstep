using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterSwim : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ActionBasedContinuousMoveProvider move))
        {
            move.enableFly = true;
            other.GetComponent<CharacterControllerDriver>().maxHeight = 1.5f;
            other.GetComponent<CharacterControllerDriver>().minHeight = 1.5f;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ActionBasedContinuousMoveProvider move))
        {
            move.enableFly = false;
            other.GetComponent<CharacterControllerDriver>().maxHeight = 2.5f;
            other.GetComponent<CharacterControllerDriver>().minHeight = 0.5f;
        }
    }
}
