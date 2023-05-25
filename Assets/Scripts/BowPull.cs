using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowPull : MonoBehaviour
{
    [SerializeField] private LineRenderer stringRenderer;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private Transform midPoint;
    [SerializeField] private Transform socket;
    [Range(0.1f, 1)] [SerializeField] private float pullLimit;
    [SerializeField] private float launchForce;
    
    private Transform _interactor;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        grabInteractable.selectEntered.AddListener(PullString);
        grabInteractable.selectExited.AddListener(ReleaseString);
    }

    private void Update()
    {
        if (_interactor)
        {
            var grabLocalPosition = midPoint.parent.InverseTransformPoint(transform.position);
            if (grabLocalPosition.x > -0.25f)
            {
                stringRenderer.SetPosition(1, midPoint.localPosition);
                socket.localPosition = new Vector3(0, 0, 0);
            }
            else if (grabLocalPosition.x > -pullLimit)
            {
                stringRenderer.SetPosition(1, new Vector3(grabLocalPosition.x, 0, 0));
                socket.localPosition = new Vector3(grabLocalPosition.x + 0.25f, 0, 0);
            }
            else
            {
                stringRenderer.SetPosition(1, new Vector3(-pullLimit, 0, 0));
                socket.localPosition = new Vector3(-pullLimit + 0.25f, 0, 0);
            }
        }
    }

    private void PullString(SelectEnterEventArgs arg0)
    {
        _interactor = arg0.interactorObject.transform;
    }
    
    private void ReleaseString(SelectExitEventArgs arg0)
    {
        _interactor = null;
        var pullStrength = CalculatePullStrength();
        if (pullStrength > 0)
        {
            LaunchArrow(pullStrength);
        }
        transform.localPosition = Vector3.zero;
        stringRenderer.SetPosition(1, midPoint.localPosition);
        socket.localPosition = new Vector3(0, 0, 0);
    }

    private float CalculatePullStrength()
    {
        return -socket.localPosition.x;
    }

    private void LaunchArrow(float pullStrength)
    {
        if (socketInteractor.interactablesSelected.Count != 0)
        {
            var arrow = socketInteractor.firstInteractableSelected;
            arrow.transform.GetComponent<Arrow>().LaunchArrow(pullStrength * launchForce);
        }
    }
}