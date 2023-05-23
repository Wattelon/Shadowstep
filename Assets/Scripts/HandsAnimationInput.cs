using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimationInput : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private InputActionProperty triggerAction;

    private const string GRIP = "Grip";
    private const string TRIGGER = "Trigger";

    private void Update()
    {
        var gripValue = gripAction.action.ReadValue<float>();
        var triggerValue = triggerAction.action.ReadValue<float>();

        animator.SetFloat(GRIP, gripValue);
        animator.SetFloat(TRIGGER, triggerValue);
    }
}