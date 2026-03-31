using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The model of the LogicGame itself. Manages the initial setup of panels and the game's functional state
/// </summary>
public interface ILogicGameModel : IModel
{
    /// <summary>
    /// Initialize the logic game's data
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - All children beneath this GameObject have Panel scripts attached to them
    ///     - No two Panels have the same coordinates
    ///     - All Panel coordinates are non-negative and are less than MAX_GRID_SIZE
    ///     - Every start endpoint has an end endpoint, and vice versa
    ///     - There are no duplicate endpoints
    /// postconditions:
    ///     - All Panels are saved in this model
    ///     - Adjacent Panels have their *Neighbor fields set where necessary
    /// </remarks>
    public void Init();

    /// <summary>
    /// Gets a panel at specific coordinates
    /// </summary>
    /// <param name="x">The X-coordinate of the panel you want</param>
    /// <param name="y">The Y-coordinate of the panel you want</param>
    /// <returns>The panel with the XY coordinates</returns>
    /// <remarks>
    /// preconditions:
    ///     - X and Y are valid coordinates
    /// postconditions:
    ///     - None
    /// </remarks>
    Panel GetPanel(int x, int y);

    /// <summary>
    /// Checks if a Panel is occupied
    /// </summary>
    /// <param name="x">The X-coordinate of the panel</param>
    /// <param name="y">The Y-coordinate of the panel</param>
    /// <returns>true if the Panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - X and Y point to a valid Panel
    /// postcondidions:
    ///     - The truth value of if the Panel is occupied or not
    /// </remarks>
    bool IsPanelOccupied(int x, int y);

    /// <summary>
    /// Clears the state of all Panels
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Every Panel has its state reset
    /// </remarks>
    void ClearGrid();

    /// <summary>
    /// Is the current grid filled? (I.e, is the game complete?)
    /// </summary>
    /// <returns>true if every Panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The truth value of whether the current grid is filled or not is returned
    /// </remarks>
    bool IsGridFilled();
}