using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [SerializeField] private Damage damage;
    [SerializeField] private XRBaseController leftController;
    [SerializeField] private XRBaseController rightController;
    [Range(0, 1)] [SerializeField] private float hapticIntensityInjury;
    [Range(0, 1)] [SerializeField] private float hapticIntensityDeath;
    [SerializeField] private float hapticDurationInjury;
    [SerializeField] private float hapticDurationDeath;

    private HealthStates _health;
    
    public void Hit(bool isCritical)
    {
        _health = damage.TakeDamage(isCritical);
        if (_health == HealthStates.Dead)
        {
            Time.timeScale = 0f;
            Debug.Log("You are dead");
            TriggerHaptic(hapticIntensityDeath, hapticDurationDeath);
        }
        else if (_health == HealthStates.Injured)
        {
            Debug.Log("You are injured");
            TriggerHaptic(hapticIntensityInjury, hapticDurationInjury);
        }
    }

    private void TriggerHaptic(float intensity, float duration)
    {
        if (intensity > 0)
        {
            leftController.SendHapticImpulse(intensity, duration);
            rightController.SendHapticImpulse(intensity, duration);
        }
    }
}
