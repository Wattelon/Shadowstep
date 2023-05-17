using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private MeshCollider weapon;
    [SerializeField] private float attackRange;
    [SerializeField] private float cooldown;

    private float _timer;
    public bool CanAttack { get; private set; }

    public float AttackRange => attackRange;

    private void Update()
    {
        if (!CanAttack)
        {
            _timer += Time.deltaTime;

            if (_timer >= cooldown)
            {
                CanAttack = true;
                _timer = 0;
            }
        }
    }

    public void TryAttackPlayer()
    {
        CanAttack = false;
    }

    public void WeaponEnable()
    {
        weapon.enabled = true;
    }
    
    public void WeaponDisable()
    {
        weapon.enabled = false;
    }
}
