using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Damage damage;

    private HealthStates _health;
    
    public void Hit(bool isCritical)
    {
        _health = damage.TakeDamage(isCritical);
        if (_health == HealthStates.Dead)
        {
            Time.timeScale = 0f;
            Debug.Log("You are dead");
        }
        else if (_health == HealthStates.Injured)
        {
            Debug.Log("You are injured");
        }
    }
}
