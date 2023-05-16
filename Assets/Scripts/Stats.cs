using UnityEngine;

public class Stats : MonoBehaviour
{
    private HealthStates _health;
    public HealthStates TakeDamage()
    {
        _health = _health == HealthStates.Untouched ? HealthStates.Injured : HealthStates.Dead;
        return _health;
    }
}
