using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    
    private Collider[] _hitColliders = new Collider[10];

    public void GenerateNoise(float sphereRadius)
    {
        var heard = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, _hitColliders, layerMask);
        for (var i = 0; i < heard; i++)
        {
            var isClear = Physics.Linecast(transform.position, _hitColliders[i].transform.position,
                LayerMask.GetMask("Obstacle", "Building"));
            if (isClear || Vector3.Distance(_hitColliders[i].transform.position, transform.position) <= sphereRadius / 2)
            {
                _hitColliders[i].GetComponentInParent<EnemyAI>().HearNoise(transform.position);
            }
        }
    }
}