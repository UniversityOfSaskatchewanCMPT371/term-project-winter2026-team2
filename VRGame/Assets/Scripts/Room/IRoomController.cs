/// <summary>
/// Interface for RoomController component.
/// </summary>
public interface IRoomController : IController
{
    

    /// <summary>
    /// Marks the minigame as complete in the model and then checks
    /// whether the room is fully complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomModel must not be null.
    /// Postconditions:
    /// - RoomModel.MinigameCompleted is set to true.
    /// - Completion logic is evaluated.
    /// Throws:
    /// - MissingFieldException if RoomModel is null.
    /// </remarks>
    void HandleCompleteMinigame();
    
    /// <summary>
    /// Marks the educational dialogue as complete in the model and then
    /// checks whether the room is fully complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomModel must not be null.
    /// Postconditions:
    /// - RoomModel.EducationalDialogueCompleted is set to true.
    /// - calls HandleCompletion() from model layer.
    /// Throws:
    /// - MissingFieldException if RoomModel is null.
    /// </remarks>
    void HandleCompleteEducationalDialogue();

    /// <summary>
    /// Checks whether the room is fully complete and, if so,
    /// notifies the view layer.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomModel must not be null.
    /// - RoomView must not be null.
    /// Postconditions:
    /// - If the model reports completion, RoomView.InvokeOnRoomComplete() is called.
    /// Throws:
    /// - MissingFieldException if RoomModel or RoomView is null.
    /// </remarks>
    void HandleCompletion();

    /// <summary>
    /// Initializes this component and verifies that the view and model
    /// references are set correctly.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Should be called after dependency injection or inspector assignment.
    /// Postconditions:
    /// - Logs errors if RoomView or RoomModel is missing.
    /// - Asserts that both references are valid.
    /// </remarks>
    new void Init();
}
