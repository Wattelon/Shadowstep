using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private LevelCompletion levelCompletion;
    
    private void OnTriggerEnter(Collider other)
    {
        var collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            levelCompletion.GainCollectable(collectable.Price);
            Destroy(collectable.gameObject);
        }
    }
}
