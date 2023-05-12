using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private const string GRIP = "Grip";
    private const string TRIGGER = "Trigger";
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private InputActionProperty triggerAction;

    private void Update()
    {
        var gripValue = gripAction.action.ReadValue<float>();
        var triggerValue = triggerAction.action.ReadValue<float>();

        animator.SetFloat(GRIP, gripValue);
        animator.SetFloat(TRIGGER, triggerValue);
    }
}