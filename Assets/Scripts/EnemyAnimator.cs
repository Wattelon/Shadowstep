using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Blocked = Animator.StringToHash("Blocked");
    private static readonly int Run = Animator.StringToHash("IsRunning");
    private static readonly int Walk = Animator.StringToHash("IsWalking");

    public void PlayAttack()
    {
        animator.SetTrigger(Attack);
    }

    public void IsRunning(bool condition)
    {
        animator.SetBool(Run, condition);
    }
    
    public void IsWalking(bool condition)
    {
        animator.SetBool(Walk, condition);
    }

    public void PlayBlocked()
    {
        animator.SetTrigger(Blocked);
    }
}
