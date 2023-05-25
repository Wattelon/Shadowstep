using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stealth : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private NoiseSource footsteps;
    
    private float _characterHeight;
    private float _visibility;
    private bool _isInNormalArea = true;

    public float Visibility => _visibility;

    private void Update()
    {
        _characterHeight = character.height;
        moveProvider.moveSpeed = _characterHeight * _characterHeight * 1.5f;
        if (_isInNormalArea)
        {
            _visibility = _characterHeight;
        }

        var hearability = character.velocity.sqrMagnitude / 3;
        if (hearability > 3)
        {
            footsteps.GenerateNoise(hearability);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            _isInNormalArea = false;
            _visibility = Mathf.Clamp(_characterHeight * 2, 0, 2);
        }
        else if (other.CompareTag("Darkness"))
        {
            _isInNormalArea = false;
            _visibility = _characterHeight * 0.5f;
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
