using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorActivation : MonoBehaviour
{
    [SerializeField] private GameObject rayObject;
    [SerializeField] private XRDirectInteractor directInteractor;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRInteractorLineVisual lineVisual;

    private void Update()
    {
        directInteractor.allowSelect = rayInteractor.interactablesSelected.Count == 0;
        lineVisual.enabled = rayInteractor.interactablesSelected.Count == 0;
        rayObject.SetActive(directInteractor.interactablesSelected.Count == 0);
    }
}
