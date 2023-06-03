using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform socket;
    
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

    public void ResetSocketPosition()
    {
        socket.localPosition = Vector3.zero;
        socket.rotation = Quaternion.identity;
    }
}