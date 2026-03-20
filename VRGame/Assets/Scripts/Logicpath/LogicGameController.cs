using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogicGameController : MonoBehaviour
{
    private bool isDragging;
    private LogicGameModel data;
    private CoordinateRef targetedPanel;
    private XRIInputActions inputActions;
    private Stack<Panel> currentPath;

    public void Awake()
    {
        isDragging = false;
        data = gameObject.GetComponent<LogicGameModel>();
        targetedPanel = null;
        inputActions = new XRIInputActions();
        currentPath = new Stack<Panel>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRIRightHandInteraction.Activate.performed += OnTriggerPress;
        inputActions.XRIRightHandInteraction.Activate.canceled += OnTriggerRelease;
        //this is not the greatest button to use for cancelling but fuck it
        inputActions.XRIRightHandInteraction.Select.performed += OnResetPress;
    }

    private void OnDisable()
    {
        inputActions.XRIRightHandInteraction.Activate.performed -= OnTriggerPress;
        inputActions.XRIRightHandInteraction.Activate.canceled -= OnTriggerRelease;
        inputActions.XRIRightHandInteraction.Select.performed -= OnResetPress;
        inputActions.Disable();
    }

    public void HandleHover(int x, int y)
    {
        targetedPanel = new CoordinateRef(x,y);
        if(isDragging)
        {
            Debug.Log("Dragging!");
            Panel hoveredPanel = data.GetPanel(targetedPanel.X, targetedPanel.Y);
            Assert.IsNotNull(hoveredPanel, "The currently-hovered panel is null");
            if(hoveredPanel.IsOccupied())
            {
                Debug.Log("But the hovered panel is occupied!");
                ClearPath();
                return;
            }
            // why can't i use a switch statement here???
            if(currentPath.Peek().LeftNeighbor != null && hoveredPanel.Equals(currentPath.Peek().LeftNeighbor)) //moving left
            {
                Debug.Log("Moving left!");
                currentPath.Peek().SetExitDirection(Direction.Left);
                hoveredPanel.SetEntryDirection(Direction.Right);
                currentPath.Push(hoveredPanel);
            }
            else if(currentPath.Peek().TopNeighbor != null && hoveredPanel.Equals(currentPath.Peek().TopNeighbor)) //moving up
            {
                Debug.Log("Moving up!");
                currentPath.Peek().SetExitDirection(Direction.Up);
                hoveredPanel.SetEntryDirection(Direction.Down);
                currentPath.Push(hoveredPanel);
            }
            else if(currentPath.Peek().RightNeighbor != null && hoveredPanel.Equals(currentPath.Peek().RightNeighbor)) //moving right
            {
                Debug.Log("Moving right!");
                currentPath.Peek().SetExitDirection(Direction.Right);
                hoveredPanel.SetEntryDirection(Direction.Left);
                currentPath.Push(hoveredPanel);
            }
            else if(currentPath.Peek().DownNeighbor != null && hoveredPanel.Equals(currentPath.Peek().DownNeighbor)) //moving down
            {
                Debug.Log("Moving down!");
                currentPath.Peek().SetExitDirection(Direction.Down);
                hoveredPanel.SetEntryDirection(Direction.Up);
                currentPath.Push(hoveredPanel);
            } else
            {
                ClearPath();
            }
        }
    }

    public void HandleUnhover(int x, int y)
    {
        if(targetedPanel != null && targetedPanel.X == x && targetedPanel.Y == y)
        {
            targetedPanel = null;
            Debug.Log("No longer hovering!");
        }
    }

    private void OnTriggerPress(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger pressed!");
        if(targetedPanel == null || data.GetPanel(targetedPanel.X, targetedPanel.Y).IsOccupied())
        {
            Debug.Log("But I can't select this panel!");
            return;
        }
        Panel hoveredPanel = data.GetPanel(targetedPanel.X, targetedPanel.Y);
        Assert.IsNotNull(hoveredPanel, "The currently-hovered panel that you pressed the trigger on is null");
        if(hoveredPanel.Attribute == PanelAttribute.Start)
        {
            Debug.Log("Starting drag!");
            currentPath.Push(hoveredPanel);
            isDragging = true;
        }
    }

    private void OnTriggerRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger released!");
    }

    private void OnResetPress(InputAction.CallbackContext context)
    {
        Debug.Log("This is where I would reset everything");
        data.ClearGrid();
    }

    private void ClearPath()
    {
        Debug.Log("Clearing path!");
        foreach(Panel panel in currentPath)
        {
            panel.ClearPanel();
        }
        currentPath.Clear();
        isDragging = false;
    }
}
