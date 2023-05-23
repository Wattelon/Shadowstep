using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowPull : MonoBehaviour
{
    [SerializeField] private LineRenderer stringRenderer;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Transform midPoint;
    [Range(0.1f, 1)] [SerializeField] private float pullLimit;
    
    private Transform interactor;

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
        if (interactor)
        {
            var grabLocalPosition = midPoint.parent.InverseTransformPoint(transform.position);
            if (grabLocalPosition.x > -0.25f)
            {
                stringRenderer.SetPosition(1, midPoint.localPosition);
            }
            else if (grabLocalPosition.x > -pullLimit)
            {
                stringRenderer.SetPosition(1, new Vector3(grabLocalPosition.x, 0, 0));
            }
            else
            {
                stringRenderer.SetPosition(1, new Vector3(-pullLimit, 0, 0));
            }
        }
    }

    private void PullString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }
    
    private void ReleaseString(SelectExitEventArgs arg0)
    {
        interactor = null;
        transform.localPosition = Vector3.zero;
        stringRenderer.SetPosition(1, midPoint.localPosition);
    }
}