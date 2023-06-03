using Pathfinding;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float minWalkableDistance;
    [SerializeField] private float maxWalkableDistance;
    [SerializeField] private float reachedPointDistance;
    [SerializeField] private float stopTargetFollowingRange;
    
    [SerializeField] private Weapon weapon;
    [SerializeField] private XRGrabInteractable[] limbsInteractables;
    [SerializeField] private Collider[] colliders;

    private Player _player;
    private Stealth _playerVisibility;
    private RichAI _richAI;
    private TargetFind _targetFind;
    private EnemyAnimator _enemyAnimator;
    private EnemyAttack _enemyAttack;
    private Damage _damage;
    
    private EnemyStates _currentState;
    private HealthStates _health;
    private Vector3 _roamPosition;

    private void Awake()
    {
        _richAI = GetComponent<RichAI>();
        _targetFind = GetComponent<TargetFind>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _damage = GetComponent<Damage>();
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerVisibility = _player.GetComponent<Stealth>();

        _currentState = EnemyStates.Roaming;

        SetRoamTarget();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyStates.Roaming:
                _enemyAnimator.IsWalking(true);
                _enemyAnimator.IsRunning(false);
                _richAI.maxSpeed = 2;
                if (Vector3.Distance(gameObject.transform.position, _roamPosition) <= reachedPointDistance)
                {
                    SetRoamTarget();
                }
                TryFindPlayer();
                break;
            case EnemyStates.Following:
                _enemyAnimator.IsWalking(false);
                _enemyAnimator.IsRunning(true);
                _richAI.maxSpeed = 5;
                _richAI.destination = _player.transform.position;
                var distance = Vector3.Distance(transform.position, _player.transform.position);
                if (distance < _enemyAttack.AttackRange)
                {
                    _enemyAnimator.IsRunning(false);
                    if (_enemyAttack.CanAttack)
                    {
                        _enemyAttack.TryAttackPlayer();
                        _enemyAnimator.PlayAttack();
                    }
                }
                else if (distance >= stopTargetFollowingRange)
                {
                    _currentState = EnemyStates.Seeking;
                }
                TryFindPlayer();
                break;
            case EnemyStates.Seeking:
                var noiseSourceSpotted = _targetFind.IsSpotted(_richAI.destination, 2);
                if (Vector3.Distance(transform.position, _richAI.destination) <= reachedPointDistance && noiseSourceSpotted)
                {
                    _currentState = EnemyStates.Roaming;
                    SetRoamTarget();
                }
                TryFindPlayer();
                break;
        }
    }

    private void TryFindPlayer()
    {
        _currentState = _targetFind.IsSpotted(_player.transform.position, _playerVisibility.Visibility) ? EnemyStates.Following : EnemyStates.Roaming;
    }
    
    private Vector3 GenerateRoamPosition()
    {
        var roamPosition = gameObject.transform.position + GenerateRandomDirection() * GenerateRandomWalkableDistance();
        roamPosition = AstarPath.active.GetNearest(roamPosition).position;
        return roamPosition;
    }

    private float GenerateRandomWalkableDistance()
    {
        var randomDistance = Random.Range(minWalkableDistance, maxWalkableDistance);
        return randomDistance;
    }

    private Vector3 GenerateRandomDirection()
    {
        var newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return newDirection.normalized;
    }

    private void SetRoamTarget()
    {
        _roamPosition = GenerateRoamPosition();
        _richAI.destination = _roamPosition;
    }
    
    public void Hit(bool isCritical)
    {
        _health = _damage.TakeDamage(isCritical);
        if (_health == HealthStates.Dead)
        {
            _richAI.enabled = false;
            GetComponent<Animator>().enabled = false;
            weapon.EnemyDead();
            foreach (var limb in limbsInteractables)
            {
                limb.enabled = true;
            }
            foreach (var col in colliders)
            {
                col.gameObject.layer = 3;
            }
        }
        else if (_health == HealthStates.Injured)
        {
            _currentState = EnemyStates.Seeking;
            _richAI.destination = _player.transform.position;
        }
    }

    public void HearNoise(Vector3 noiseSource)
    {
        _currentState = EnemyStates.Seeking;
        _richAI.destination = noiseSource;
    }
}