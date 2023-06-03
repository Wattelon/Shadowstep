using UnityEngine;

public class TargetFind : MonoBehaviour
{
    [SerializeField] private float visionRange;
    [SerializeField] private float fieldOfView;
    [SerializeField] private LayerMask obstacles; 
    
    public bool IsSpotted(Vector3 target, float visibility)
    {
        var distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget / visibility <= visionRange)
        {
            var targetDirection = (transform.position - target).normalized;
            if (Vector3.Angle(targetDirection, -transform.forward) < fieldOfView / 2)
            {
                if (!Physics.Linecast(transform.position, target, obstacles))
                {
                    return true;
                }
            }
        }
        return false;
    }
}