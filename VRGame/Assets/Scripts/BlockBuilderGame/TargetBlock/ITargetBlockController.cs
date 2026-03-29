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
}
