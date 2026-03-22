using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the logic of the logicgame, taking in inputs and changing panels as needed
/// </summary>
public class LogicGameController : MonoBehaviour //TODO: implement IController
{
    /// <summary>
    /// Are we in a valid, dragging state?
    /// </summary>
    private bool isDragging;
    /// <summary>
    /// The data of the game (panels)
    /// </summary>
    private LogicGameModel data;
    /// <summary>
    /// What are the coordinates of the panel we're aiming at, if any?
    /// </summary>
    private CoordinateRef targetedPanel;
    /// <summary>
    /// InputActions reference
    /// </summary>
    private XRIInputActions inputActions;
    /// <summary>
    /// The current path we're taking
    /// </summary>
    private Stack<Panel> currentPath;

    /// <summary>
    /// Unity Awake() method, gets initial state set up
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - A LogicGameModel script is attached to this object
    /// postconditions:
    ///     - All variables are initialized
    /// </remarks>
    public void Awake()
    {
        isDragging = false;
        data = gameObject.GetComponent<LogicGameModel>();
        if(data == null)
        {
            Debug.LogError("There is no LogicGameModel attached to this GameObject!");
        }
        Assert.IsNotNull(data, "There is no LogicGameModel attached to this GameObject!");
        targetedPanel = null;
        inputActions = new XRIInputActions();
        currentPath = new Stack<Panel>();
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

#pragma warning disable IDE0051
    /// <summary>
    /// Handles a hover event from a Panel, changing the game state where necessary
    /// </summary>
    /// <param name="x">The X coordinate of a Panel</param>
    /// <param name="y">The Y coordinate of a Panel</param>
    /// <remarks>
    /// preconditions:
    ///     - X and Y must be valid coordinates (i.e, 0 <= X,Y < LogicGameModel.MAX_GRID_SIZE, must point to a valid Panel in the LogicGameModel)
    /// postconditions:
    ///     - If we're not dragging, then no post-conditions
    ///     - If we are dragging:
    ///     - Cancels the current drag and resets the path if we change our hover to an occupied Panel
    ///     - Cancels the current drag and resets the path if we change our hover to a non-adjacent Panel
    ///     - Continues the current drag if we change our hover to an adjacent, non-occupied Panel
    /// </remarks>
    public void HandleHover(int x, int y)
    {
        targetedPanel = new CoordinateRef(x,y);
        if(isDragging)
        {
            Debug.Log("Dragging!");
            Panel hoveredPanel = data.GetPanel(targetedPanel.X, targetedPanel.Y);
            if(hoveredPanel == null)
            {
                Debug.LogError("The currently-hovered panel is apparently null");
            }
            Assert.IsNotNull(hoveredPanel, "The currently-hovered panel is apparently null");
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
            } else //the hovered Panel is not adjacent to the previous Panel in our path
            {
                isDragging = false;
                ClearPath();
            }
        }
    }

    /// <summary>
    /// Handles the end of hovering on a Panel
    /// </summary>
    /// <param name="x">The X coordinate of a Panel</param>
    /// <param name="y">The Y coordinate of a Panel</param>
    /// <remarks>
    /// preconditions:
    ///     - X and Y must be valid coordinates
    /// postconditions:
    ///     - If we're not hovering on a new panel, clear the targetedPanel coordinates
    /// </remarks>
    public void HandleUnhover(int x, int y)
    {
        if(targetedPanel != null && targetedPanel.X == x && targetedPanel.Y == y)
        {
            targetedPanel = null;
            Debug.Log("No longer hovering!");
        }
    }

    /// <summary>
    /// Handles pressing the (right) trigger
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     - We are not pressing the right trigger
    /// postconditions:
    ///     - If we're hovering over a non-occupied Panel, begin a drag movement
    ///     - Otherwise, do nothing
    /// </remarks>
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

    /// <summary>
    /// Handles releasing the (right) trigger
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     We are holding down the right trigger
    /// postconditions:
    ///     - If we are not in a dragging state, do nothing
    ///     - If our drag ends on an endpoint, complete the drag movement
    ///     - If our drag doesn't end on an endpoint (or any Panel), cancel the drag movement and clear the path
    /// </remarks>
    private void OnTriggerRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger released!");
        if(isDragging && (targetedPanel == null || currentPath.Peek().Attribute != PanelAttribute.Exit))
        {
            ClearPath();
        }
        else if(isDragging && targetedPanel != null && currentPath.Peek().Attribute == PanelAttribute.Exit && data.IsGridFilled())
        {
            //TODO: make a proper celebration
            Debug.Log("Game is complete!");
        }
        isDragging = false;
    }

    /// <summary>
    /// Handles pressing the designated reset button
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The game state is reset
    /// </remarks>
    private void OnResetPress(InputAction.CallbackContext context)
    {
        Debug.Log("Resetting game state...");
        data.ClearGrid();
    }

    /// <summary>
    /// Clear the current path we're drawing
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The current path being drawn is cleared by resetting the panels in the path and the path stack
    /// </remarks>
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
