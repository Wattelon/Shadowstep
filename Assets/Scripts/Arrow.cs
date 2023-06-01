using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : MonoBehaviour
{
    private bool _inAir;
    private Rigidbody _rigidbody;
    private XRGrabInteractable _grabInteractable;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void LaunchArrow(float force)
    {
        _grabInteractable.interactionLayers = 0;
        _rigidbody.AddForce(transform.up * force);
        _inAir = true;
        _trailRenderer.enabled = true;
        StartCoroutine(RotateInAir());
    }

    private void FixedUpdate()
    {
        if (_grabInteractable.interactorsSelecting.Count != 0)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_inAir && !other.collider.isTrigger)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Direct", "Ray", "Arrow");
            _trailRenderer.enabled = false;
            transform.SetParent(other.transform);
            _inAir = false;
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
    }

    private IEnumerator RotateInAir()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            transform.up = _rigidbody.velocity;
            yield return null;
        }
    }
}