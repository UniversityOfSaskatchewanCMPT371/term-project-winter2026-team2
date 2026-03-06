using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    private void StartDrag()
    {
        Vector3 rayOrigin = GetRayOrigin();
        Panel panel = iGridModel.GetPanelAtWorldPosition(rayOrigin);

        if (panel == null)
        {
            Debug.LogWarning("Drag started outside grid");
            return;
        }

        isDragging = true;
        dragPath.Clear();
        dragPath.Add(panel);
        previousPanel = panel;

        Debug.Log($"Drag started at ({panel.GridX}, {panel.GridY})");
    }

    private void ContinueDrag()
    {
        Vector3 rayOrigin = GetRayOrigin();
        Panel currentPanel = iGridModel.GetPanelAtWorldPosition(rayOrigin);

        if (currentPanel == null)
        {
            return;
        }

        if (currentPanel == previousPanel)
        {
            return;
        }

        dragPath.Add(currentPanel);
        PlacePipeSegment(previousPanel, currentPanel);
        previousPanel = currentPanel;
        
    }

    private void EndDrag()
    {
        isDragging = false;
        previousPanel = null;

        if (CheckCompleted())
        {
            iGridView.ShowCompletionEffect();
        }
    }

    private void PlacePipeSegment(Panel fromPanel, Panel toPanel)
    {
        Direction entryDirection = GetDirection(fromPanel, toPanel);
        Direction exitDirection = GetOppositeDirection(entryDirection);

        bool placed = iGridModel.TryPlacePipe(
            toPanel.GridX,
            toPanel.GridY,
            entryDirection,
            exitDirection,
            currentPipeColor
        );

        if (placed)
        {
            iGridView.RenderPipe(
                toPanel.GridX,
                toPanel.GridY,
                currentPipeColor,
                toPanel.WorldPosition
            );

            Debug.Log($"Pipe placed at ({toPanel.GridX}, {toPanel.GridY})");
        }
    }

    private Direction GetDirection(Panel fromPanel, Panel toPanel)
    {
        int fromX = fromPanel.GridX;
        int fromY = fromPanel.GridY;
        int toX = toPanel.GridX;
        int toY = toPanel.GridY;

        if (toX > fromX) return Direction.Right;
        if (toX < fromX) return Direction.Left;
        if (toY > fromY) return Direction.Up;
        if (toY < fromY) return Direction.Down;

        return Direction.None;
    }

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

        isDragging = false;
        dragPath.Clear();
        previousPanel = null;

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

    public void SetPipeColor(Color color)
    {
        currentPipeColor = color;
        Debug.Log($"Pipe color changed to {color}");
    }

}