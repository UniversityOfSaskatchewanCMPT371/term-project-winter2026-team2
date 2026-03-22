using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Model portion of the grid module for logic path minigame.
/// </summary>
public interface ILogicGameModel
{
    /// <summary>
    /// Gets the GridCell object at the specified coordinates
    /// <param name="x">X-coordinate in grid</param>
    /// <param name="y">Y-coordinate in grid</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   x and y are valid coordinates within grid bounds
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns the GridCell object at the specified coordinates, 
    ///         or null if coordinates are out of bounds
    /// </post-condition>
    /// </remarks>
    Panel GetPanel(int x, int y);

    /// <summary>
    ///  Checks if a cell is occupied by a pipe
    /// <param name="x">The x-coordinate of the cell to check</param>
    /// <param name="y">The y-coordinate of the cell to check</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   x and y are valid coordinates
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns true if the cell is occupied, false otherwise
    /// </post-condition>
    /// </remarks>
    bool IsPanelOccupied(int x, int y);

    /// <summary>
    /// Removes all pipes from grid
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   None
    /// </pre-condition>
    /// <post-condition>
    ///     -   All cells are empty
    /// </post-condition>
    /// </remarks>
    void ClearGrid();

    /// <summary>
    /// Checks if all cells are filled
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   Grid has been initialized
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns true if all cells are occupied by pipes, false otherwise
    /// </post-condition>
    /// </remarks>
    bool IsGridFilled();
}