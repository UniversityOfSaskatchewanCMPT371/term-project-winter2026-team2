using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// The controller for the logicpath minigame
/// </summary>
public class GridController : MonoBehaviour, IGridController
{

    [SerializeField] private GridModel gridModel;
    [SerializeField] private GridView gridView;
    [SerializeField] private XRRayInteractor rightRayInteractor;
    [SerializeField] private InputActionProperty rightTriggerAction;
    [SerializeField] private InputActionProperty resetButtonAction;

    private IGridModel iGridModel;
    private IGridView iGridView;
    private Color currentPipeColor = Color.red;

    private List<Panel> dragPath = new List<Panel>();
    private Panel previousPanel = null;
    private bool isDragging = false;
    
    /// <inheritdoc/>
    public void Awake()
    {
        Assert.IsNotNull(gridModel, "GridModel must be assigned");
        Assert.IsNotNull(gridView, "GridView must be assigned");

        // Using interfaces
        iGridModel = gridModel;
        iGridView = gridView;

        if (rightRayInteractor == null)
        {
            rightRayInteractor = GetComponentInChildren<XRRayInteractor>();
        }

        Debug.Log("GridController initialized");
    }

    public void Update()
    {
        // Checking if trigger is pressed
        bool triggerPressed = rightTriggerAction.action?.IsPressed() ?? false;

        if (triggerPressed && !isDragging)
        {
            StartDrag();
        }
        else if (triggerPressed && isDragging)
        {
            ContinueDrag();
        }
        else if (!triggerPressed && isDragging)
        {
            EndDrag();
        }

        if (resetButtonAction.action?.WasPressedThisFrame() ?? false)
        {
            ResetGrid();
        }
    }

    /// <summary>
    /// Begins a drag operation when trigget is pressed
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     - Trigger is pressed
    ///     - Is not already dragging
    /// </pre-condition>
    /// <post-condition>
    ///     - Dragging starts
    ///     - If outside the grid, dragging does not start
    /// </post-condition>
    /// </remarks>
    private void StartDrag()
    {
        // Get the ray origin
        Vector3 rayOrigin = GetRayOrigin();
        Panel panel = iGridModel.GetPanelAtWorldPosition(rayOrigin);

        if (panel == null)
        {
            Debug.LogWarning("Drag started outside grid");
            return;
        }

        // Enter dragging mode
        isDragging = true;
        dragPath.Clear();
        dragPath.Add(panel);
        previousPanel = panel;

        Debug.Log($"Drag started at ({panel.GridX}, {panel.GridY})");
    }

    /// <summary>
    /// Continue's the dragging operation as the trigger is being held
    /// </summary>
    /// /// <remarks>
    /// <pre-condition>
    ///     - isDragging is true
    ///     - Trigger is still pressed
    /// </pre-condition>
    /// <post-condition>
    ///     - If moved to a new cell, pipe is placed
    ///     - If on same cell, nothing happens
    /// </post-condition>
    /// </remarks>
    private void ContinueDrag()
    {
        // Get the ray origin
        Vector3 rayOrigin = GetRayOrigin();
        Panel currentPanel = iGridModel.GetPanelAtWorldPosition(rayOrigin);

        // If the ray went outside the grid
        if (currentPanel == null)
        {
            return;
        }

        // If the ray is still on the same cell
        if (currentPanel == previousPanel)
        {
            return;
        }

        // Move to the new cell
        dragPath.Add(currentPanel);
        PlacePipeSegment(previousPanel, currentPanel);
        previousPanel = currentPanel;
        
    }

    /// <summary>
    /// Ends the dragging operation
    /// </summary>
    /// /// <remarks>
    /// <pre-condition>
    ///     -isDragging is true
    ///     - Trigger is released
    /// </pre-condition>
    /// <post-condition>
    ///     - isDragging turns false
    ///     - Checks if puzzle is completed
    /// </post-condition>
    /// </remarks>
    private void EndDrag()
    {
        // Exiting drag mode
        isDragging = false;
        previousPanel = null;

        // Checks if the puzzle is completed
        if (CheckCompleted())
        {
            iGridView.ShowCompletionEffect();
        }
    }

    /// <summary>
    /// Places a pipe depending on movement
    /// </summary>
    /// <param name="fromPanel">The panel the drag came from</param>
    /// <param name="toPanel">The panel that's being dragged to</param>
    /// /// <remarks>
    /// <pre-condition>
    ///     - fromPanel and toPanel are adjacent
    ///     - toPanel is not occupied
    /// </pre-condition>
    /// <post-condition>
    ///     - If successful: places a pipe
    ///     - If not: view and model is unchanged
    /// </post-condition>
    /// </remarks>
    private void PlacePipeSegment(Panel fromPanel, Panel toPanel)
    {
        // Determine which direction the drag came from
        Direction entryDirection = GetDirection(fromPanel, toPanel);
        Direction exitDirection = GetOppositeDirection(entryDirection);

        // Try to place the pipe
        bool placed = iGridModel.TryPlacePipe(
            toPanel.GridX,
            toPanel.GridY,
            entryDirection,
            exitDirection,
            currentPipeColor
        );

        // If pipe is placed, render it
        if (placed)
        {
            iGridView.RenderPipe(
                toPanel.GridX,
                toPanel.GridY,
                currentPipeColor,
                toPanel.WorldPosition,
                toPanel
            );

            Debug.Log($"Pipe placed at ({toPanel.GridX}, {toPanel.GridY})");
        }
    }

    /// <summary>
    /// Determines which direction to move from one panel to another
    /// </summary>
    /// <param name="fromPanel">Starting panel</param>
    /// <param name="toPanel">Destination panel</param>
    /// /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - Returns the direction if panels are adjacent
    ///     - Returns None if not
    /// </post-condition>
    /// </remarks>
    private Direction GetDirection(Panel fromPanel, Panel toPanel)
    {
        int fromX = fromPanel.GridX;
        int fromY = fromPanel.GridY;
        int toX = toPanel.GridX;
        int toY = toPanel.GridY;

        // Checking which direction moved
        if (toX > fromX) return Direction.Right;
        if (toX < fromX) return Direction.Left;
        if (toY > fromY) return Direction.Up;
        if (toY < fromY) return Direction.Down;

        return Direction.None;
    }

    /// <summary>
    /// Gets the opposite direction (used to calculate exit direction)
    /// </summary>
    /// <param name="direction"></param>
    /// /// <remarks>
    /// <pre-condition>
    ///     - None
    /// </pre-condition>
    /// <post-condition>
    ///     - Returns the opposite direction is panels are adjacent
    ///     - Returns None if not
    /// </post-condition>
    /// </remarks>
    private Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default: return Direction.None;
        }
    }

    /// <inheritdoc/>
    public void ResetGrid()
    {
        Assert.IsNotNull(iGridModel, "Model must be initialized");
        Assert.IsNotNull(iGridView, "View must be initialized");

        // Exit drag mode if active
        isDragging = false;
        dragPath.Clear();
        previousPanel = null;

        // Clear the model and the view
        iGridModel.ClearGrid();
        iGridView.ClearAllPipes();

        Debug.Log("Grid reset");
    }

    /// <inheritdoc/>
    public bool CheckCompleted()
    {
        Assert.IsNotNull(iGridModel, "Model must be initialized");
        return iGridModel.IsGridFilled();
    }

    /// <summary>
    /// Gets the ray origin from the XR controller
    /// </summary>
    /// /// <remarks>
    /// <pre-condition>
    ///     - rightRayInteractor is assigned
    ///     - Grid is initialized
    /// </pre-condition>
    /// <post-condition>
    ///     - Returns the point in grid where the ray intersects
    /// </post-condition>
    /// </remarks>
    private Vector3 GetRayOrigin()
    {
        if (rightRayInteractor != null)
        {
            return rightRayInteractor.rayOriginTransform.position;
        }

        if (transform != null)
        {
            return transform.position;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// Sets the color of the placed pipe
    /// (Forgot to put in interface before freeze)
    /// </summary>
    /// <param name="color">Color for the pipe</param>
    /// <remarks>
    /// <pre-condition>
    ///     - Color is a valid color
    /// </pre-condition>
    /// <post-condition>
    ///     - currentPipe color is updated
    /// </post-condition>
    /// </remarks>
    public void SetPipeColor(Color color)
    {
        currentPipeColor = color;
        Debug.Log($"Pipe color changed to {color}");
    }

}