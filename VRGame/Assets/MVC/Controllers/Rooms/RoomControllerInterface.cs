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
}
