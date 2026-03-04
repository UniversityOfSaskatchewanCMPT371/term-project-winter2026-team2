using UnityEngine;

/// <summary>
/// Represents a single grid cell for the logic path minigame.
/// </summary>
public class GridCell
{
    /// <summary>
    /// X coordinate in the grid
    /// </summary>
    public int gridX
    {
        get;
        set;
    }

    /// <summary>
    /// Y coordinate in the grid
    /// </summary>
    public int gridY
    {
        get;
        set;
    }

    /// <summary>
    /// World position of the cell in the game world
    /// </summary>
    public Vector3 worldPosition
    {
        get;
        set;
    }

    /// <summary>
    /// Color of the pipe in this cell, or null if there is no pipe
    /// </summary>
    public Color? pipeColor
    {
        get;
        set;
    }
}