
/// <summary>
/// Default internal interface for room controller.
/// </summary>
public interface InternalRoomController
{
    // DATA SECTION

    /// <summary>
    /// Getter/Setter for view layer component.
    /// </summary>
    public IRoomView RoomView {
        /// <summary>
        /// Retrieves the view layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remarks>
        /// Precondtion:
        /// - roomView or roomViewMock is not null.
        /// Postcondition:
        /// - returns the reference to view layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to view layer component.
        /// </returns>
        get; 
        /// <summary>
        /// Modifies the reference to the view layer component, or the
        /// mock, if the new value inherits from View class.
        /// </summary>
        /// <remarks>
        /// Precondition:
        /// - Value is not null.
        /// - Value either inherits from View class, or
        /// a mock.
        /// Postcondition:
        /// - Reference to the view layer is modified.
        /// </remarks>
        set; 
        }

    /// <summary>
    /// Getter/Setter for model layer component
    /// </summary>
    public IRoomModel RoomModel {
        /// <summary>
        /// Retrieves the model layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remark>
        /// Preconditions:
        /// - roomModel or roomModelMock is not null.
        /// Postconditions:
        /// - returns the reference to model layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to model layer component.
        /// </return>
        get;
        /// <summary>
        /// Modifies the reference to the model layer component, or the
        /// mock, if the new value inherits from Model class.
        /// </summary>
        /// <remarks>
        /// Precondition:
        /// - Value is not null.
        /// - Value is either inherits from Model class, or
        /// a mock.
        /// Postcondition:
        /// - Reference to the model layer is modified.
        /// </remarks>
        set; 
        }
}

/// <summary>
/// Default external interface for room controller.
/// </summary>
public interface IRoomController
{
    /// METHODS SECTION

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
    void Init();
}
