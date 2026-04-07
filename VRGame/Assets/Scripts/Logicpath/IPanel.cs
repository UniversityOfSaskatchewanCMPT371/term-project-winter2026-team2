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
    
    /// <summary>
    /// Accessor for top neighbour Panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the top neighbor of this Panel is returned if it exists, null otherwise
    ///     - If setting, the top neighbor of this Panel is written
    /// </remarks>
    public Panel TopNeighbor {get; set;}

    /// <summary>
    /// Accessor for right neighbour panel
    /// </summary>
    public Panel RightNeighbor {get; set;}

    /// <summary>
    /// Accessor for down neighbour panel
    /// </summary>
    public Panel DownNeighbor {get; set;}

    /// <summary>
    /// Accessor for left neighbour panel
    /// </summary>
    public Panel LeftNeighbor {get; set;}
    
    /// <summary>
    /// Accessor for panel colour
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the colour of this Panel is returned
    ///     - If setting, the colour of this Panel is overwritten, and the texture is refreshed to reflect the state
    /// </remarks>
    public PanelColour PanelColour {get; set;}
    
    /// <summary>
    /// Accessor for this Panel's X-coordinate
    /// </summary>
    public int GridX {get; set;}
    
    /// <summary>
    /// Accessor for grid Y coordinate
    /// </summary>
    public int GridY {get; set;}
    
    /// <summary>
    /// Setter for entry direction
    /// </summary>
    /// <param name="entryDirection">The new entry direction</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - This panel's entryDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </remarks>
    public void SetEntryDirection(Direction entryDirection);
    
    /// <summary>
    /// Setter for exit direction
    /// </summary>
    /// <param name="exitDirection">The new exit direction</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - This panel's exitDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </remarks>

    public void SetExitDirection(Direction exitDirection);
    
    /// <summary>
    /// Accessor for Panel attribute (normal, start, exit, block)
    /// </summary>
    /// </remarks>
    public PanelAttribute Attribute {get; set;}
}
