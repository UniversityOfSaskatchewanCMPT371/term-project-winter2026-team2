using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// View portion of the grid module for logic path minigame.
/// </summary>
public interface IGridView
{

    /// <summary>
    /// Initializes the grid view with the specified dimensions and cell size.
    /// <param name="width">Grid width in cells</param>
    /// <param name="height">Grid height in cells</param>
    /// <param name="cellSize">Size of each cell in world units</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Width, height, and cell size are > 0
    /// </pre-condition>
    /// <post-condition>
    ///    -   Visual of grid is created and rendered
    /// </post-condition>
    /// </remarks>
    void Initialize(int width, int height, float cellSize);

    /// <summary>
    /// Renders a pipe segment at the specified grid coordinates with the given color and world position.
    /// <param name="x">X coordinate in grid space</param>
    /// <param name="y">Y coordinate in grid space</param>
    /// <param name="color">Color of the pipe to render</param>
    /// <param name="worldPosition">World position of the pipe segment</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   gridX and gridY are within the bounds of the grid
    ///    -   color is a valid Color object
    ///    -   worldPosition is a valid Vector3
    ///    -   Grid is initialized
    /// </pre-condition>
    /// <post-condition>
    ///    -   A pipe segment of the specified color is rendered at the given grid coordinates
    ///    -   The pipe is rendered with specified colour
    /// </post-condition>
    /// </remarks>
    void RenderPipe(int x, int y, Color color, Vector3 worldPosition, Panel panel);

    /// <summary>
    /// Renders the endpoints on the grid based on the provided list of Endpoint objects.
    /// <param name="endpoints">List of Endpoint objects to render</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Grid is initialized
    ///    -   Endpoints list is not null
    ///    -   Endpoints contains valid Endpoint objects
    /// </pre-condition>
    /// <post-condition>
    ///    -   Each endpoint in the list is rendered on the grid at its specified coordinates
    ///    -   Each endpoint is rendered with its specified colour
    /// </post-condition>
    /// </remarks>
    void RenderEndpoints(List<Endpoint> endpoints);

    /// <summary>
    /// Clears all pipe segments from the grid, leaving only the endpoints visible.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Grid is initialized
    /// </pre-condition>
    /// <post-condition>
    ///    -   All pipe segments are removed from the grid, leaving only endpoints visible
    /// </post-condition>
    /// </remarks>
    void ClearAllPipes();

    /// <summary>
    /// Shows completion effect on the grid when the puzzle is solved.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Grid is initialized
    ///    -   Puzzle is solved
    /// </pre-condition>
    /// <post-condition>
    ///    -   A visual effect/animation is displayed on the grid to indicate completion
    /// </post-condition>
    /// </remarks>
    void ShowCompletionEffect();

    /// <summary>
    /// Highlights the grid cell at the specified world position to indicate it is being interacted with.
    /// <param name="position">World position of the cell to highlight</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   position is a valid Vector3 corresponding to a cell in the grid
    ///     -   Grid is initialized
    /// </pre-condition>
    /// <post-condition>
    ///     -   Grid cell at position is visually highlighted
    /// </post-condition>
    /// </remarks>
    void HighlightCell(Vector3 position);

    /// <summary>
    /// Removes highlights from the view
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Grid is initialized
    /// </pre-condition>
    /// <post-condition>
    ///    -   All highlights are removed from the grid view
    /// </post-condition>
    /// </remarks>
    void ClearHighlight();
}