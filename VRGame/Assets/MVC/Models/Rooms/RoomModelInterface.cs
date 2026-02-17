
/// <summary>
/// Default interface for room model.
/// </summary>
public interface IRoomModel
{
    /// DATA SECTION

    /// <summary>
    /// Unique identifier of this room.
    /// </summary>
    int Id { 
        /// <summary>
        /// Access the current id of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondition:
        /// - Returns the current id of the room.
        /// </remarks>
        /// <returns>
        /// Current id of the room.
        /// </returns>
        get;
        }

    /// <summary>
    /// Name of this room.
    /// </summary>
    string Name {
        /// <summary>
        /// Access the current name of this room
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondition:
        /// - Returns the current name of the room.
        /// </remarks>
        /// <returns>
        /// Current name of the room.
        /// </returns>
        get;
        }

    /// <summary>
    /// Current completion state of minigame.
    /// </summary>
    static bool MinigameCompleted
    {
        get;

        set;
    }

    /// <summary>
    /// Current completion state of educational dialogue.
    /// </summary>
    static bool EducationalDialogueCompleted
    {
        /// <summary>
        /// Access the current completion state of educational dialogue.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondtitions:
        /// - Returns the current static
        /// </remarks>
        get;

        set;
    }

    /// METHODS SECTION

    /// <summary>
    /// Verifies if the room is complete or not.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the current completion state of the room.
    /// </remarks>
    /// <returns>Current completion state of the room.</returns>
    bool IsComplete();

    /// <summary>
    /// Changes the state of the room to complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Educational dialogue is complete
    /// - Minigame is complete.
    /// Postconditions:
    /// - Completion state is set to true.
    /// </remarks>
    /// <returns></returns>
    void Complete();
}
