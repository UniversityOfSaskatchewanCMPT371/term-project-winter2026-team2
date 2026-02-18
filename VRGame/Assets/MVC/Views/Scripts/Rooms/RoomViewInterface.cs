public interface IRoomView
{
    /// DATA SECTION

    /// <summary>
    /// Called when all educational dialogues and minigame is completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The room is in a valid state of compeletion.
    /// Postconditions:
    /// - All subscribed listeners to this event is executed.
    /// </remarks>
    event System.Action OnRoomCompleted;

    /// METHODS SECTIONS

    /// <summary>
    /// Called when all educational dialogues are complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - All educational dialogues are completed.
    /// Postconditions:
    /// - The room's educational dialogue completion state is updated.
    /// </remarks>
    void EducationalDialoguesCompleted();

    /// <summary>
    /// Called when the minigame is completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Minigame is completed.
    /// Postconditions:
    /// - The room's minigame compeltion state is updated.
    /// </remarks>
    void MinigameCompleted();

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized fields must be set, or default.
    /// - Reference to controller layer must be set.
    /// Postcondtions:
    /// - Asserts that all Serialized fields are in a valid.
    /// - Asserts that the reference to controller layer is valid.
    /// </remarks>
    void Init();
}
