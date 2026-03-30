/// <summary>
/// Interface for the CheckButton Controller.
/// </summary>
public interface ICheckButtonController : IController
{
    /// <summary>
    /// Calls Init to check for component references
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures Init() is called
    /// </remarks>
    new void Awake();

    /// <summary>
    /// Reference checks for model, view, TargetBlockController, and CheckAreaController
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (modelInstance != null) && (viewInstance != null) && (targetBlockController != null)
    /// post-condition:
    ///     - ensures all references are checked
    /// </remarks>
    new void Init();

    /// <summary>
    /// Counts the SnapFitControllers of the colliders in the CheckArea and calls CheckCompletion on the TargetBlockController with the found blocks
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (modelInstance != null) && (viewInstance != null) && (targetBlockController != null)
    /// post-condition:
    ///     - ensures TargetBlockController.CheckCompletion is called (with the found blocks in the check area)
    /// </remarks>
    void OnButtonPressed();
}
