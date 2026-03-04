using UnityEngine;

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
}