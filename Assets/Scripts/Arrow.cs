using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : MonoBehaviour
{
    private InputAction launch = new("launch", binding: "<Keyboard>/l");
    private bool _inAir;
    private Rigidbody _rigidbody;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void LaunchArrow(float force)
    {
        _grabInteractable.interactionLayers = 0;
        _rigidbody.AddForce(transform.up * force);
        _inAir = true;
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
            _grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Direct", "Arrow");
            transform.SetParent(other.transform);
            _inAir = false;
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
