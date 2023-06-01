using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            if (other.transform.root.TryGetComponent(out EnemyAnimator enemyAnimator))
            {
                enemyAnimator.PlayBlocked();
            }
        }
    }
}