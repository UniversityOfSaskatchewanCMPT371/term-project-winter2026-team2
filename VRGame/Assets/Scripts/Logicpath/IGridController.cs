using System.Numerics;
using UnityEngine;

/// <summary>
/// Controller portion of the grid module for logic path minigame.
/// </summary>
public interface IGridController
{

    /// <summary>
    /// Initializes the grid model and view with the specified dimensions and cell size.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///    -   Model and View are not null
    /// </pre-condition>
    /// <post-condition>
    ///   -   Controller is ready to recieve input
    /// </post-condition>
    /// </remarks>
    void Awake();

    /// <summary>
    /// Handles input when a trigger is pressed at a specific world position.
    /// <param name="worldPosition">The world position where the trigger was pressed</param>
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///   -   Controller is initialized
    ///   -   worldPosition is a valid Vector3
    /// </pre-condition>
    /// <post-condition>
    ///   -   If the trigger press corresponds to a valid grid cell, the model is
    ///       updated to reflect the new pipe segment, and the view is updated to render the change
    /// </post-condition>
    /// </remarks>
    void OnTriggeredPressed(Vector3 worldPosition);

    /// <summary>
    /// Resets the grid to its initial state, clearing all pipes and connections.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///   -   Awake() has been called
    ///   -   Model and View are initialized
    /// </pre-condition>
    /// <post-condition>
    ///  -   Grid is cleared of all pipes and connections, and returned to its initial state
    /// </post-condition>
    /// </remarks>
    void ResetGrid();

    /// <summary>
    /// Checks if all endpoints are correctly connected according to the game rules.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///  -   Model is initialized
    /// </pre-condition>
    /// <post-condition>
    ///  -   Returns true if all endpoints are correctly connected, false otherwise
    /// </post-condition>
    /// </remarks>
    bool CheckCompleted();
}