using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        if (isEnemy)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Hit(false);
            }
        }
    }

    public void EnemyDead()
    {
        isEnemy = false;
        GetComponent<XRGrabInteractable>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
        transform.SetParent(null);
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}