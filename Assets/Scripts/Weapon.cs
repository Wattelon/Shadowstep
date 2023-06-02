using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isEnemy;

    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //_grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        //StartCoroutine(ReselectWeapon());
        if (isEnemy) return;
        if (other.impulse.sqrMagnitude < 50) return;
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

    private void OnCollisionExit(Collision other)
    {
        //_grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
        //StartCoroutine(ReselectWeapon());
    }

    private void OnTriggerEnter(Collider other)
    {
        var col = other.gameObject;
        if (isEnemy && col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Hit(false);
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

    IEnumerator ReselectWeapon()
    {
        _grabInteractable.enabled = false;
        //_grabInteractable.firstInteractorSelecting.transform.GetComponent<XRDirectInteractor>().enabled = false;
        yield return new WaitForFixedUpdate();
        _grabInteractable.enabled = true;
        //_grabInteractable.firstInteractorSelecting.transform.GetComponent<XRDirectInteractor>().enabled = true;
    }
}