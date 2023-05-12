using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            health = 0;
            Debug.Log("Died");
            return;
        }

        health -= damage;
    }
}
