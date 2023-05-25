using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSocket : MonoBehaviour
{
    private XRSocketInteractor _socketInteractor;

    private void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void Start()
    {
        _socketInteractor.selectEntered.AddListener(OnWeaponSelected);
        _socketInteractor.selectExited.AddListener(OnWeaponDeselected);
    }

    private void OnWeaponSelected(SelectEnterEventArgs arg0)
    {
        arg0.interactableObject.transform.GetComponent<Collider>().isTrigger = true;
    }
    
    private void OnWeaponDeselected(SelectExitEventArgs arg0)
    {
        arg0.interactableObject.transform.GetComponent<Collider>().isTrigger = false;
    }
}
