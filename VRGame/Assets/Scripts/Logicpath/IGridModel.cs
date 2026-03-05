using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Model portion of the grid module for logic path minigame.
/// </summary>
public interface IGridModel
{
 
 /// <summary>
 ///  Public accessor method for grid width
 /// </summary>
 /// 
 int GridWidth
    {
        /// <summary>
        /// Getter method for grid width
        /// </summary>
        /// <remarks>
        /// <pre-condition>
        ///     -   Grid has been initialized
        /// </pre-condition>
        /// <post-condition>
        ///     -   Returns the grid width value
        /// </post-condition>
        /// </remarks>
        get;
    }

    /// <summary>
    /// Public accessor method for grid height
    /// </summary>
    int GridHeight
    {
        /// <summary>
        /// Getter method for grid height
        /// </summary>
        /// <remarks>
        /// <pre-condition>
        ///     -   Grid has been initialized
        /// </pre-condition>
        /// <post-condition>
        ///     -   Returns the grid height value
        /// </post-condition>
        /// </remarks>
        get;
    }

    /// <summary>
    /// Public accessor method for cell size 
    /// </summary>
    float CellSize
    {
        /// <summary>
        /// Gets the size of each cell in the grid in world units
        /// </summary>
        /// <remarks>
        /// <pre-condition>
        ///     -   Grid has been initialized
        /// </pre-condition>
        /// <post-condition>
        ///     -   Returns the cell size value
        /// </post-condition>
        /// </remarks>
        get;
    }

    List<Endpoint> Endpoints
    {
        /// <summary>
        /// Gets the list of endpoints in the grid
        /// </summary>
        /// <remarks>
        /// <pre-condition>
        ///     -   Grid has been initialized
        /// </pre-condition>
        /// <post-condition>
        ///     -   Returns a list of all endpoints in the grid
        /// </post-condition>
        /// </remarks>
        get;
    }

    /// <summary>
    /// Method to initialize the grid model with specified parameters
    /// </summary>
    /// <param name="width">Width of the grid in cells</param>
    /// <param name="height">Height of the grid in cells</param>
    /// <param name="cellSize">Size of each cell in world units</param>
    /// <remarks>
    /// <pre-condition>
    ///     -   Width and height must be > 0
    ///     -   Cell size must be > 0
    /// </pre-condition>
    /// <post-condition>
    ///     -   Grid model is initialized with all cells empty
    ///     -   World positions for each cell are calculated
    /// </post-condition>
    /// </remarks>
    void Initialize(int width, int height, float cellSize);

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
    /// Attempts to place a pipe of the specified color at the given cell coordinates
    /// <param name="x">X-coordinate in grid</param>
    /// <param name="y">Y-coordinate in grid</param>
    /// <param name="pipeColor">Color of the pipe to place</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   x and y are valid coordinates
    ///     -   pipeColor is a valid color </pre-condition>
    /// <post-condition>
    ///     -   If the cell is unoccupied, a pipe of the specified color is placed and the method returns true
    ///     -   If the cell is occupied, no changes are made and the method returns false
    /// </post-condition>
    /// </remarks>
    bool TryPlacePipe(int x, int y, Color pipeColor);

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

    /// <summary>
    /// Adds an endpoint to the grid model
    /// <param name="endpoint">The endpoint to add</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   endpoint is not null
    ///     -   endpoint coordinates are within grid bounds
    /// </pre-condition>
    /// <post-condition>
    ///     -   The endpoint is added to the list of endpoints
    /// </post-condition>
    /// </remarks>
    void AddEndpoint(Endpoint endpoint);

    /// <summary>
    /// Gets the world position of a grid cell
    /// <param name="x">X-coordinate in grid</param>
    /// <param name="y">Y-coordinate in grid</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   x and y are valid coordinates within grid bounds
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns the world position of the cell
    /// </post-condition>
    /// </remarks>
    Vector3 GetWorldPosition(int x, int y);


    /// <summary>
    /// Gets the grid cell at the specified world position
    /// <param name="worldPosition">Position in world space</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   worldPosition is a valid Vector3
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns the closest GridCell at the specified world position,
    ///         or null if the position is outside the grid bounds
    /// </post-condition>
    /// </remarks>
    Panel GetPanelAtWorldPosition(Vector3 worldPosition);
}