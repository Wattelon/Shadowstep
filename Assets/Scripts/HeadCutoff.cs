using UnityEngine;

public class HeadCutoff : MonoBehaviour
{
    [SerializeField] private Rigidbody headRigidbody;
    [SerializeField] private float power;

    private const string WEAPON_TAG = "Weapon";

    private void OnTriggerEnter(Collider col)
    {
        
        if (!col.CompareTag(WEAPON_TAG))
        {
            return;
        }
        Debug.Log("trigger");
        headRigidbody.isKinematic = false;
        headRigidbody.AddForce(Vector3.up * power);
    }
}