using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;

    private Player _player;
    private float _timer;
    public bool CanAttack { get; private set; }

    public float AttackRange => attackRange;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

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
        _player.TakeDamage(damage);
        CanAttack = false;
    }
}
