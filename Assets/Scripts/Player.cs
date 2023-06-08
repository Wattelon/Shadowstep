using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcess;
    [SerializeField] private PostProcessProfile injuryProfile;
    [SerializeField] private LevelCompletion level;
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
            TriggerHaptic(hapticIntensityDeath, hapticDurationDeath);
            level.FailLevel();
        }
        else if (_health == HealthStates.Injured)
        {
            TriggerHaptic(hapticIntensityInjury, hapticDurationInjury);
            postProcess.profile = injuryProfile;
        }
    }

    public void TriggerHaptic(float intensity, float duration)
    {
        if (intensity > 0 && duration > 0)
        {
            leftController.SendHapticImpulse(intensity, duration);
            rightController.SendHapticImpulse(intensity, duration);
        }
    }
}