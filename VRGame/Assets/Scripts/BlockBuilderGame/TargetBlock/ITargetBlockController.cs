/// <summary>
/// Interface for the TargetBlock Controller component.
/// </summary>
public interface ITargetBlockController : IController
{
    /// <summary>
    /// Calls Init to check references and counts target block children transforms
    /// <summary>
    /// Checks references of model and view components.
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (modelInstance != null) && (viewInstance != null)
    /// post-condition:
    ///     - ensures targetBlocks.Length > 0
    /// </remarks>
    new void Init();

    /// <summary>
    /// Checks whether the player's current built blocks match the target block
    /// </summary>
    /// <param name="builtBlocks">The player's current built blocks</param>
    /// <remarks>
    /// pre-condition:
    ///     - requires builtBlocks != null
    /// post-condition:
    ///     - ensures builtBlocks are compared to target block configuration && angles are compared with tolerance
    /// </remarks>
    void CheckCompletion(SnapFitController[] builtBlocks);
}
