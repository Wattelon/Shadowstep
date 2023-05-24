using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : MonoBehaviour
{
    private BoxCollider _collider;
    private XRGrabInteractable _grabInteractable;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }
    
    void Update()
    {
        _collider.isTrigger = _grabInteractable.interactorsSelecting.Count != 0;
    }
}