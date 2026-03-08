using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

/// <summary>
/// The model for the logic path minigame.
/// </summary>
public class GridModel : MonoBehaviour, IGridModel
{
    private Panel[,] grid;
    private int gridWidth;
    private int gridHeight;
    private float cellSize;
    private List<Endpoint> endpoints = new List<Endpoint>();

    /// <inheritdoc/>
    public int GridWidth
    {
        get
        {
            return gridWidth;
        }
    }

    /// <inheritdoc/>
    public int GridHeight
    {
        get
        {
            return gridHeight;
        }
    }


    /// <inheritdoc/>
    public float CellSize
    {
        get
        {
            return cellSize;
        }
    }

    /// <inheritdoc/>
    public List<Endpoint> Endpoints
    {
        get
        {
            return endpoints;
        }
    }

    /// <inheritdoc/>
    public void Initialize(int width, int height, float cellSize)
    {
        
        Assert.IsTrue(width > 0, "Grid width must be > 0");
        Assert.IsTrue(height > 0, "Grid height must be > 0");
        Assert.IsTrue(cellSize > 0, "Cell size must be > 0");

        this.gridWidth = width;
        this.gridHeight = height;
        this.cellSize = cellSize;

        grid = new Panel[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate world position
                Vector3 worldPos = GetWorldPosition(x, y);

                // Create a game object for each cell of the grid
                GameObject panelGO = new GameObject($"Panel_{x}_{y}");
                panelGO.transform.parent = transform;
                panelGO.transform.position = worldPos;

                // Adding panel components from Panel.cs
                Panel panel = panelGO.AddComponent<Panel>();
                panel.Initialize(x, y, worldPos);

                // Store it in the grid
                grid[x, y] = panel;
            }
        }

        Debug.Log($"GridModel initialized: {width}x{height}, cell size {cellSize}");
    }

    /// <inheritdoc/>
    public Panel GetPanel(int x, int y)
    {
        // Checking the bounds
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return grid[x, y];
        }
        return null; // If out of bounds, return null
    }

    /// <inheritdoc/>
    public bool IsPanelOccupied(int x, int y)
    {
        Panel panel = GetPanel(x, y);
        return panel != null && panel.IsOccupied();
    }

    /// <inheritdoc/>
    public bool TryPlacePipe(int x, int y, Direction entryDirection, Direction exitDirection, Color pipeColor)
    {
        // Get the panel at the coordinates
        Panel panel = GetPanel(x, y);

        // Check if panel exists
        if (panel == null)
        {
            Debug.LogWarning($"Panel ({x}, {y}) does not exist");
            return false;
        }

        // Check if there is a pipe already
        if (IsPanelOccupied(x, y))
        {
            Debug.LogWarning($"Panel ({x}, {y}) is already occupied");
            return false;
        }

        Assert.IsTrue(entryDirection != Direction.None, "Entry direction cannot be None");
        Assert.IsTrue(exitDirection != Direction.None, "Exit direction cannot be None");
        
        // Place pipe on panel
        panel.SetPipeDirection(entryDirection, exitDirection);
        panel.PipeColor = pipeColor;

        return true;
    }

    /// <inheritdoc/>
    public void ClearGrid()
    {
        Assert.IsNotNull(grid, "Grid must be initialized");

        // Clear for each cell
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y].ClearPanel();
            }
        }
    }

    /// <inheritdoc/>
    public bool IsGridFilled()
    {
        Assert.IsNotNull(grid, "Grid must be initialized");

        // Checking every cell in grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (!IsPanelOccupied(x, y))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <inheritdoc/>
    public void AddEndpoint(Endpoint endpoint)
    {
        Assert.IsNotNull(endpoint, "Endpoint cannot be null");
        Assert.IsTrue(endpoint.GridX >= 0 && endpoint.GridX < gridWidth, "Endpoint GridX is out of bounds");
        Assert.IsTrue(endpoint.GridY >= 0 && endpoint.GridY < gridHeight, "Endpoint GridY is out of bounds");


        endpoints.Add(endpoint);
    }

    /// <inheritdoc/>
    public Vector3 GetWorldPosition(int x, int y)
    {
        Assert.IsTrue(x >= 0 && x < gridWidth, "X coordinate is out of bounds");
        Assert.IsTrue(y >= 0 && y < gridHeight, "Y coordinate is out of bounds");

        // Calculating offsets to center the grid at the origin
        float offsetX = (gridWidth * cellSize) / 2f;
        float offsetY = (gridHeight * cellSize) / 2f;

        // Return the centered world position
        return new Vector3(
            x * cellSize - offsetX, 
            y * cellSize - offsetY,
            0f
        );
    }

    /// <inheritdoc/>
    public Panel GetPanelAtWorldPosition(Vector3 worldPosition)
    {
        Assert.IsNotNull(grid, "Grid must be initialized");

        Panel closestPanel = null;
        float minDistance = CellSize * 0.6f; // Snapping distance threshold

        // Finding the closest panel within snapping distance
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Calculate distance
                float distance = Vector3.Distance(worldPosition, grid[x, y].WorldPosition);
                if (distance < minDistance) // If distance is closer than previous distance
                {
                    minDistance = distance;
                    closestPanel = grid[x, y];
                }
            }
        }

        return closestPanel;
    }
}