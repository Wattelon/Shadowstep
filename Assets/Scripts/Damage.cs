using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float intervalBetweenHits;
    
    private HealthStates _health;
    
    private float _damageTimer;
    private bool _hitable;
    
    private void Update()
    {
        if (!_hitable)
        {
            _damageTimer += Time.deltaTime;
            if (_damageTimer >= intervalBetweenHits)
            {
                _hitable = true;
                _damageTimer = 0;
            }
        }
    }

    public HealthStates TakeDamage(bool isCritical)
    {
        if (!_hitable) return _health; 
        if (_health == HealthStates.Dead) return _health;
        _health = isCritical || _health == HealthStates.Injured ? HealthStates.Dead : HealthStates.Injured;
        _hitable = false;
        return _health;
    }
}