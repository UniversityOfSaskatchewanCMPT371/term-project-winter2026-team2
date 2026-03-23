using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IPanel
{
    /// <summary>
    /// Checks if the panel is occupied, whether by a line or by its own block
    /// </summary>
    /// <returns>true if the panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The truth value of whether or not this panel is occupied is returned
    /// </remarks>
    public bool IsOccupied();

    /// <summary>
    /// Clears any line status from this panel, resetting entry and exit directions
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Resets entry and exit directions to None
    ///     - Texture is refreshed
    /// </remarks>
    public void ClearPanel();

    /// <summary>
    /// Event listener function for hover start events
    /// </summary>
    /// <param name="args">Arguments for this event</param>
    void OnHoverEntered(HoverEnterEventArgs args);

    /// <summary>
    /// Event listener function for hover end events
    /// </summary>
    /// <param name="args">Arguments for this event</param>
    void OnHoverExited(HoverExitEventArgs args);
}
