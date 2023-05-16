using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    
    private void OnCollisionEnter(Collision other)
    {
        if (isEnemy) return;
        var col = other.gameObject;
        if (col.CompareTag("NormalHit"))
        {
            var enemy = col.transform.GetComponentInParent(typeof(EnemyAI));
            enemy.GetComponent<EnemyAI>().Hit(false);
        }
        else if (col.CompareTag("CriticalHit"))
        {
            var enemy = col.transform.GetComponentInParent(typeof(EnemyAI));
            enemy.GetComponent<EnemyAI>().Hit(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var col = other.gameObject;
        Debug.Log(col.name);
        if (isEnemy)
        {
            if (col.CompareTag("Player"))
            {
                
                col.GetComponent<Player>().Hit(false);
            }
        }
    }
}