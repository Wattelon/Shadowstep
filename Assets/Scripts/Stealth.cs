using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Stealth : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private NoiseSource footsteps;
    [SerializeField] private Slider visibilityTracker;
    [SerializeField] private Slider hearabilityTracker;
    
    private float _characterHeight;
    private bool _isInNormalArea = true;

    public float Visibility { get; private set; }

    private void Update()
    {
        _characterHeight = character.height;
        moveProvider.moveSpeed = _characterHeight * _characterHeight * 1.5f;
        if (_isInNormalArea)
        {
            Visibility = _characterHeight;
        }

        var hearability = character.velocity.sqrMagnitude / 3;
        if (hearability > 3)
        {
            footsteps.GenerateNoise(hearability);
        }

        visibilityTracker.value = Visibility;
        hearabilityTracker.value = hearability;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isInNormalArea = false;
            Visibility = Mathf.Clamp(_characterHeight * 2, 0, 2);
        }
        else if (other.CompareTag("Darkness"))
        {
            _isInNormalArea = false;
            Visibility = _characterHeight * 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Darkness") || other.CompareTag("Light"))
        {
            _isInNormalArea = true;
        }
    }
}