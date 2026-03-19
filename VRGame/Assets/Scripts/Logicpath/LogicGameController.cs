using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogicGameController : MonoBehaviour
{
    private bool isDragging;
    private LogicGameModel logicGameModel;
    private CoordinateRef targetedPanel;
    private XRIInputActions inputActions;

    public void Awake()
    {
        isDragging = false;
        logicGameModel = gameObject.GetComponent<LogicGameModel>();
        targetedPanel = null;
        inputActions = new XRIInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRIRightHandInteraction.UIPress.performed += OnTriggerPress;
        inputActions.XRIRightHandInteraction.UIPress.canceled += OnTriggerRelease;
    }

    private void OnDisable()
    {
        inputActions.XRIRightHandInteraction.UIPress.performed -= OnTriggerPress;
        inputActions.XRIRightHandInteraction.UIPress.canceled -= OnTriggerRelease;
        inputActions.Disable();
    }

    public void HandleHover(int x, int y)
    {
        targetedPanel = new CoordinateRef(x,y);
    }

    public void HandleUnhover(int x, int y)
    {
        if(targetedPanel != null && targetedPanel.X == x && targetedPanel.Y == y)
        {
            targetedPanel = null;
        }
    }

    private void OnTriggerPress(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger pressed!");
    }

    private void OnTriggerRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger released!");
    }

    private void OnResetPress(InputAction.CallbackContext context)
    {
        Debug.Log("This is where I would reset everything");
    }
}
