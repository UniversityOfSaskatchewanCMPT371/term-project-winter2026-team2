using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the logic of the logicgame, taking in inputs and changing panels as needed
/// </summary>
public class LogicGameController : Controller<ILogicGameModel, Panel>, ILogicGameController
{
    /// <summary>
    /// Are we in a valid, dragging state?
    /// </summary>
    private bool isDragging;
    /// <summary>
    /// What are the coordinates of the panel the left controller is aiming at, if any?
    /// </summary>
    private CoordinateRef targetedPanelLeft;
    /// <summary>
    /// What are the coordinates of the panel the right controller is aiming at, if any?
    /// </summary>
    private CoordinateRef targetedPanelRight;
    /// <summary>
    /// InputActions reference
    /// </summary>
    private XRIInputActions inputActions;
    /// <summary>
    /// The current path the left hand is taking
    /// </summary>
    private Stack<Panel> currentPathLeft;
    /// <summary>
    /// The current path the right hand is taking
    /// </summary>
    private Stack<Panel> currentPathRight;

    /// <inheritdoc/>
    public override void Init()
    {
        isDragging = false;
        modelInstance = gameObject.GetComponent<LogicGameModel>();
        if(modelInstance == null)
        {
            Debug.LogError("There is no LogicGameModel attached to this GameObject!");
        }
        Assert.IsNotNull(modelInstance, "There is no LogicGameModel attached to this GameObject!");
        targetedPanelRight = null;
        inputActions = new XRIInputActions();
        currentPathRight = new Stack<Panel>();
    }

    /// <summary>
    /// Unity Awake() method - gets initial interaction state set up
    /// </summary>
    public void Awake()
    {
        Init();
    }

#pragma warning disable IDE0051 //no, these methods ARE used by Unity, c#.
    /// <summary>
    /// Unity OnEnable() method, initializes InputActions mapping
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - XR controller action event listeners are mapped to their functions
    /// </remarks>
    private void OnEnable()
    {
        inputActions.Enable();
        //TODO: handle the left controller's actions
        inputActions.XRIRightHandInteraction.Activate.performed += OnTriggerPress;
        inputActions.XRIRightHandInteraction.Activate.canceled += OnTriggerRelease;
        //this is not the greatest button to use for cancelling but fuck it
        inputActions.XRIRightHandInteraction.Select.performed += OnResetPress;
    }

    /// <summary>
    /// Unity OnDisable() method, tears down InputActions mapping
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - XR controller action event listeners are already mapped to their functions (there is no way this ISN'T the case)
    /// postconditions:
    ///     - XR controller action event listeners are unmapped from their functions
    /// </remarks>
    private void OnDisable()
    {
        inputActions.XRIRightHandInteraction.Activate.performed -= OnTriggerPress;
        inputActions.XRIRightHandInteraction.Activate.canceled -= OnTriggerRelease;
        inputActions.XRIRightHandInteraction.Select.performed -= OnResetPress;
        inputActions.Disable();
    }
#pragma warning restore IDE0051

    /// <inheritdoc/>
    public void HandleHover(int x, int y)
    {
        targetedPanelRight = new CoordinateRef(x,y);
        if(isDragging)
        {
            Debug.Log("Dragging!");
            Panel hoveredPanel = modelInstance.GetPanel(targetedPanelRight.X, targetedPanelRight.Y);
            if(hoveredPanel == null)
            {
                Debug.LogError("The currently-hovered panel is apparently null");
            }
            Assert.IsNotNull(hoveredPanel, "The currently-hovered panel is apparently null");
            if(hoveredPanel.GridX == currentPathRight.Peek().GridX && hoveredPanel.GridY == currentPathRight.Peek().GridY)
            {
                Debug.LogWarning("Somehow you're re-hovering on the same panel as before, which doesn't really have any effect. We're just going to exit out of the hover function entirely and preserve the dragging state");
                return;
            }
            if(hoveredPanel.IsOccupied())
            {
                Debug.Log(hoveredPanel);
                Debug.Log("But the hovered panel is occupied!");
                ClearPath();
                return;
            }
            if(hoveredPanel.PanelColour != currentPathRight.Peek().PanelColour && hoveredPanel.Attribute != PanelAttribute.Normal)
            {
                Debug.Log("But we're trying to enter an endpoint of the wrong colour!");
                ClearPath();
                return;
            }
            // why can't i use a switch statement here???
            if(currentPathRight.Peek().LeftNeighbor != null && hoveredPanel.Equals(currentPathRight.Peek().LeftNeighbor)) //moving left
            {
                Debug.Log("Moving left!");
                currentPathRight.Peek().SetExitDirection(Direction.Left);
                hoveredPanel.SetEntryDirection(Direction.Right);
                hoveredPanel.PanelColour = currentPathRight.Peek().PanelColour;
                currentPathRight.Push(hoveredPanel);
            }
            else if(currentPathRight.Peek().TopNeighbor != null && hoveredPanel.Equals(currentPathRight.Peek().TopNeighbor)) //moving up
            {
                Debug.Log("Moving up!");
                currentPathRight.Peek().SetExitDirection(Direction.Up);
                hoveredPanel.SetEntryDirection(Direction.Down);
                hoveredPanel.PanelColour = currentPathRight.Peek().PanelColour;
                currentPathRight.Push(hoveredPanel);
            }
            else if(currentPathRight.Peek().RightNeighbor != null && hoveredPanel.Equals(currentPathRight.Peek().RightNeighbor)) //moving right
            {
                Debug.Log("Moving right!");
                currentPathRight.Peek().SetExitDirection(Direction.Right);
                hoveredPanel.SetEntryDirection(Direction.Left);
                hoveredPanel.PanelColour = currentPathRight.Peek().PanelColour;
                currentPathRight.Push(hoveredPanel);
            }
            else if(currentPathRight.Peek().DownNeighbor != null && hoveredPanel.Equals(currentPathRight.Peek().DownNeighbor)) //moving down
            {
                Debug.Log("Moving down!");
                currentPathRight.Peek().SetExitDirection(Direction.Down);
                hoveredPanel.SetEntryDirection(Direction.Up);
                hoveredPanel.PanelColour = currentPathRight.Peek().PanelColour;
                currentPathRight.Push(hoveredPanel);
            } else //the hovered Panel is not adjacent to the previous Panel in our path
            {
                Debug.Log("But the hover changed to a non-adjacent Panel!");
                isDragging = false;
                ClearPath();
            }
        }
    }

    /// <inheritdoc/>
    public void HandleUnhover(int x, int y)
    {
        if(targetedPanelRight != null && targetedPanelRight.X == x && targetedPanelRight.Y == y)
        {
            targetedPanelRight = null;
            Debug.Log("No longer hovering!");
        }
    }

    /// <inheritdoc/>
    public void OnTriggerPress(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger pressed!");
        if(targetedPanelRight == null)
        {
            Debug.Log("But I'm not aiming at a panel!");
            return;
        }
        Panel hoveredPanel = modelInstance.GetPanel(targetedPanelRight.X, targetedPanelRight.Y);
        if(hoveredPanel == null)
        {
            Debug.LogError($"The panel we're trying press on ({targetedPanelRight.X},{targetedPanelRight.Y}) is apparently null!");
        }
        Assert.IsNotNull(hoveredPanel, $"The panel we're trying to press on ({targetedPanelRight.X},{targetedPanelRight.Y}) is apparently null!");
        if(hoveredPanel.IsOccupied())
        {
            Debug.Log("But the panel I'm aiming at is occupied!");
            return;
        }
        if(hoveredPanel.Attribute == PanelAttribute.Start)
        {
            Debug.Log("Starting drag!");
            currentPathRight.Push(hoveredPanel);
            isDragging = true;
        }
    }

    /// <inheritdoc/>
    public void OnTriggerRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger released!");
        if(isDragging && (targetedPanelRight == null || currentPathRight.Peek().Attribute != PanelAttribute.Exit))
        {
            ClearPath();
        }
        else if(isDragging && targetedPanelRight != null && currentPathRight.Peek().Attribute == PanelAttribute.Exit && modelInstance.IsGridFilled())
        {
            //TODO: make a proper celebration
            Debug.Log("Game is complete!");
        }
        isDragging = false;
    }

    /// <inheritdoc/>
    public void OnResetPress(InputAction.CallbackContext context)
    {
        if(targetedPanelRight == null)
        {
            return;
        }
        Debug.Log("Resetting game state...");
        modelInstance.ClearGrid();
    }

    /// <inheritdoc/>
    public void ClearPath()
    {
        Debug.Log("Clearing path!");
        foreach(Panel panel in currentPathRight)
        {
            panel.ClearPanel();
        }
        currentPathRight.Clear();
        isDragging = false;
    }
}
