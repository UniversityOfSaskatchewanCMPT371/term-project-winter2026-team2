/// <summary>
/// Default internal interface for room view.
/// </summary>
public interface InternalRoomView
{
    /// DATA SECTION
    
    /// <summary>
    /// Getter/Setter for controller layer component.
    /// </summary>
    public IRoomController RoomController{
        /// <summary>
        /// Retrieves the controller layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - roomController or roomControllerMock is not null.
        /// Postconditions:
        /// - Returns the reference to controller layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to the controller layer component.
        /// </returns>
        get; 
        /// <summary>
        /// Modifies the reference to the controller layer component, or the
        /// mock, if the new value inherits from Controller class.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not null
        /// - Value either inherits from Controller class, or a mock.
        /// Postconditions:
        /// - Reference to the controller layer is modified.
        /// </remarks>
        set; 
        }
}

/// <summary>
/// Default external interface for room view.
/// </summary>
public interface IRoomView
{
    /// METHODS SECTIONS
    
    /// <summary>
    /// Invokes all listeners subscribed to OnRoomComplete event.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The room is in a valid state of completion
    /// Postconditions:
    /// - All listeners subscribed to onRoomCompleted are invoked.
    /// </remarks>
    void InvokeOnRoomComplete();

    /// <summary>
    /// Called when the state of the minigame component is finished and completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomController is not null.
    /// - Minigame component is in a state of finished and completed.
    /// Postconditions:
    /// - The room's model layer minigameCompletion field gets updated.
    /// Throws:
    /// - MissingFieldException is thrown if RoomController is null.
    /// </remarks>
    void MinigameCompleted();

    /// <summary>
    /// Called when the state of the EducationalDialogue component is finished and completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomController is not null.
    /// - EducationalDialogue component is in a state of finished and completed.
    /// Postconditions:
    /// - The room's model layer educationalDialogueCompletion field gets updated.
    /// Throws:
    /// - MissingFieldException is thrown if RoomController is null
    /// </remarks>
    void EducationalDialoguesCompleted();

    /// <summary>
    /// Initializes this component and verifies that the controller
    /// reference is set correctly.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Should be called after dependency injection or inspector assignment.
    /// Postconditions:
    /// - Logs errors if RoomController is missing.
    /// - Asserts that the references are valid.
    /// </remarks>
    void Init();
}
