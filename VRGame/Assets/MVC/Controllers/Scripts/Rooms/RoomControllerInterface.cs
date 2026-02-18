public interface IRoomController
{
    /// METHODS SECTION

    /// <summary>
    /// Handles the logic required when the room becomes completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The room is not previously completed.
    /// - All educational dialogue completed.
    /// - Minigame is completed.
    /// Postconditions:
    /// - The room's completion state is updated.
    /// - Any completion-related effects or callbacks have been executed.
    /// </remarks>
    void HandleCompletion();

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Reference to view and model layers must be set.
    /// Postcondtions:
    /// - Asserts that the references to the view and model layers are valid.
    /// </remarks>
    void Init();
}
