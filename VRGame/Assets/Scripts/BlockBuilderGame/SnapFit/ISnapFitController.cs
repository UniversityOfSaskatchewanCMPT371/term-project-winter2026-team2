/// <summary>
/// Interface for the SnapFit Controller.
/// Manages snap logic for a block on release.
/// </summary>
public interface ISnapFitController : IController
{
    /// <summary>
    /// Checks references of model and view components.
    /// Also sets snap points in the model by finding child components named "Top" or "Bottom".
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (modelInstance != null) && (viewInstance != null)
    /// post-condition:
    ///    - ensures snap points are set in the model
    /// </remarks>
    new void Init();

    /// <summary>
    /// Detaches the block from any snapped position by destroying the snap joint and updating the model state
    /// </summary>
    /// <remarks> 
    /// pre-condition:
    ///     - requires modelInstance != null
    /// post-condition:
    ///     - ensures (any existing snap joint is destroyed) && (model state is updated to not snapped)
    /// </remarks>
    void Detach();

    /// <summary>
    /// Attempts to snap the block to a nearby snap point if within range, by creating a fixed joint and updating the model state
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires modelInstance != null
    /// post-condition:
    ///     - ensures (block prefabs present in the scene are snapped together if within radiues range) &&
    ///       (model state is updated to reflect snapped position)
    /// </remarks>
    void Snap();

    /// <summary>
    /// Checks whether name1 and name2 are a valid snap point pair (one starts with "Top" and the other starts with "Bottom")
    /// </summary>
    /// <param name="name1">Name of the first snap point</param>
    /// <param name="name2">Name of the second snap point</param>
    /// <remarks>
    /// pre-condition:
    ///     - requires (name1 != null) && (name2 != null)
    /// post-condition:
    ///     - ensures returns true if name1 and name2 are a valid snap point pair, false otherwise
    /// </remarks>
    bool IsMatch(string name1, string name2);

    /// <summary>
    /// Breaks the snap joint and updates the model state when the block is grabbed
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires modelInstance != null
    /// post-condition:
    ///     - ensures (modelInstance.SnapJoint == null) && (modelInstance.IsSnapped == false)
    /// </remarks>
}
