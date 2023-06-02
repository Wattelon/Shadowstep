using UnityEngine;

public class LevelDestinationArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LevelCompletion>().CompleteLevel();
        }
    }
}
