using UnityEngine;
using UnityEngine.InputSystem;

public interface ILogicGameController
{
    /// <summary>
    /// Gets initial interaction state set up
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - A LogicGameModel script is attached to this object
    /// postconditions:
    ///     - All variables are initialized
    /// </remarks>
    public void Init();

    /// <summary>
    /// Handles a hover event from a Panel, changing the game state where necessary
    /// </summary>
    /// <param name="x">The X coordinate of a Panel</param>
    /// <param name="y">The Y coordinate of a Panel</param>
    /// <remarks>
    /// preconditions:
    ///     - X and Y are non-negative
    ///     - X and Y are than LogicGameModel.MAX_GRID_SIZE
    ///     - X and Y point to a panel in data
    /// postconditions:
    ///     - If we're not dragging, then no post-conditions
    ///     - If we are dragging:
    ///     - Cancels the current drag and resets the path if we change our hover to an occupied Panel
    ///     - Cancels the current drag and resets the path if we change our hover to a non-adjacent Panel
    ///     - Continues the current drag if we change our hover to an adjacent, non-occupied Panel
    /// </remarks>
    public void HandleHover(int x, int y);

    /// <summary>
    /// Handles the end of hovering on a Panel
    /// </summary>
    /// <param name="x">The X coordinate of a Panel</param>
    /// <param name="y">The Y coordinate of a Panel</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If we're not hovering on a new panel, clear the targetedPanel coordinates
    /// </remarks>
    public void HandleUnhover(int x, int y);

    /// <summary>
    /// Handles pressing the (right) trigger
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If we're hovering over a non-occupied Panel, begin a drag movement
    ///     - Otherwise, do nothing
    /// </remarks>
    void OnTriggerPress(InputAction.CallbackContext context);

    /// <summary>
    /// Handles releasing the (right) trigger
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If we are not in a dragging state, do nothing
    ///     - If our drag ends on an endpoint, complete the drag movement
    ///     - If our drag doesn't end on an endpoint (or any Panel), cancel the drag movement and clear the path
    /// </remarks>
    void OnTriggerRelease(InputAction.CallbackContext context);

    /// <summary>
    /// Handles pressing the designated reset button
    /// </summary>
    /// <param name="context">The CallbackContext for this action</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The game state is reset
    /// </remarks>
    void OnResetPress(InputAction.CallbackContext context);

    /// <summary>
    /// Clear the current path we're drawing
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The current path being drawn is cleared by resetting the panels in the path and the path stack
    /// </remarks>
    void ClearPath();
}
