using Pathfinding;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float minWalkableDistance;
    [SerializeField] private float maxWalkableDistance;

    [SerializeField] private float reachedPointDistance;

    [SerializeField] private GameObject roamTarget;

    [SerializeField] private float targetFollowRange;
    [SerializeField] private float stopTargetFollowingRange;

    [SerializeField] private EnemyAttack enemyAttack;

    [SerializeField] private AIDestinationSetter aiDestinationSetter;

    [SerializeField] private EnemyAnimator enemyAnimator;

    [SerializeField] private AIPath aiPath;

    [SerializeField] private Damage damage;
    [SerializeField] private Weapon weapon;
    [SerializeField] private XRGrabInteractable[] limbsInteractables;
    [SerializeField] private Collider[] colliders;

    [SerializeField] private TargetFind targetFind;

    private Player _player;
    private Stealth _playerVisibility;

    private EnemyStates _currentState;
    private HealthStates _health;
    private Vector3 _roamPosition;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerVisibility = _player.GetComponent<Stealth>();

        _currentState = EnemyStates.Roaming;

        _roamPosition = GenerateRoamPosition();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyStates.Roaming:
                enemyAnimator.IsWalking(true);
                enemyAnimator.IsRunning(false);
                aiPath.maxSpeed = 2;
                if (Vector3.Distance(gameObject.transform.position, _roamPosition) <= reachedPointDistance)
                {
                    _roamPosition = GenerateRoamPosition();
                    roamTarget.transform.position = _roamPosition;
                    aiDestinationSetter.target = roamTarget.transform;
                }
                TryFindPlayer();
                break;
            case EnemyStates.Following:
                enemyAnimator.IsWalking(false);
                enemyAnimator.IsRunning(true);
                aiPath.maxSpeed = 5;
                aiDestinationSetter.target = _player.transform;
                var distance = Vector3.Distance(transform.position, _player.transform.position);
                if (distance < enemyAttack.AttackRange)
                {
                    enemyAnimator.IsRunning(false);
                    if (enemyAttack.CanAttack)
                    {
                        enemyAttack.TryAttackPlayer();
                        enemyAnimator.PlayAttack();
                    }
                }
                else if (distance >= stopTargetFollowingRange)
                {
                    _currentState = EnemyStates.Seeking;
                }
                TryFindPlayer();
                break;
            case EnemyStates.Seeking:
                var noiseSourceSpotted = targetFind.IsSpotted(roamTarget.transform, 2);
                if (Vector3.Distance(transform.position, roamTarget.transform.position) <= reachedPointDistance && noiseSourceSpotted)
                {
                    _currentState = EnemyStates.Roaming;
                    _roamPosition = GenerateRoamPosition();
                    roamTarget.transform.position = _roamPosition;
                    aiDestinationSetter.target = roamTarget.transform;
                }
                TryFindPlayer();
                break;
        }
    }

    private void TryFindPlayer()
    {
        _currentState = targetFind.IsSpotted(_player.transform, _playerVisibility.Visibility) ? EnemyStates.Following : EnemyStates.Roaming;
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
        var newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        return newDirection.normalized;
    }
    
    public void Hit(bool isCritical)
    {
        _health = damage.TakeDamage(isCritical);
        if (_health == HealthStates.Dead)
        {
            Debug.Log("Enemy dead");
            aiPath.enabled = false;
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
            Debug.Log("Enemy injured");
        }
    }

    public void HearNoise(Vector3 noiseSource)
    {
        _currentState = EnemyStates.Seeking;
        roamTarget.transform.position = noiseSource;
    }
}