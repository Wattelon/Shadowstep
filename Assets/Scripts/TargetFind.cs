using UnityEngine;

public class TargetFind : MonoBehaviour
{
    [SerializeField] private float visionRange;
    [SerializeField] private float fieldOfView;
    [SerializeField] private LayerMask obstacles; 
    
    public bool IsSpotted(Transform target, float visibility)
    {
        var distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget / visibility <= visionRange)
        {
            var targetDirection = (transform.position - target.position).normalized;
            if (Vector3.Angle(targetDirection, -transform.forward) < fieldOfView / 2)
            {
                if (!Physics.Linecast(transform.position, target.position, obstacles))
                {
                    return true;
                }
            }
        }
        return false;
    }
}