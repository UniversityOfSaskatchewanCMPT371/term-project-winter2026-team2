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