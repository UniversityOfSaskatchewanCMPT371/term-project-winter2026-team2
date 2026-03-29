/// <summary>
/// Interface for the TargetBlock View.
/// Handles visual feedback when the player completes the target.
/// </summary>
public interface ITargetBlockView : IView
{
    /// <summary>
    /// Checks references of controller component
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-condition:
    ///     - ensures controllerInstance is checked
    /// </remarks>
    new void Init();

    /// <summary>
    /// Displays completion feedback on target block completion
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures completion feedback is displayed (currently a debug log message)
    /// </remarks>
    void OnComplete();
}
