
/// <summary>
/// Default internal interface for room model.
/// </summary>
public interface InternalRoomModel
{
    
}

/// <summary>
/// Default external interface for room model.
/// </summary>
public interface IRoomModel
{
    /// DATA SECTION

    /// <summary>
    /// Getter/Setter for this room's unique id.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    ///
    /// Postconditions:
    /// - Getting returns the current room id.
    /// - Setting updates the room id.
    /// </remarks>
    int Id { 
        get;
        set;
        }

    /// <summary>
    /// Getter/Setter for this room's name.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Setting value must not be null.
    /// - Setting value must not match the current name.
    /// Postconditions:
    /// - Getting returns the current room name.
    /// - Setting updates the room name.
    /// Throws:
    /// - InvalidOperationException if the new value matches the current name.
    /// </remarks>
    string Name {
        get;
        set;
        }

    /// <summary>
    /// Marks the minigame as complete and triggers completion logic.
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
    bool MinigameCompleted
    {
        get;
        set;
    }

    /// <summary>
    /// Marks the educational dialogue as complete and triggers completion logic.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomModel must not be null.
    /// Postconditions:
    /// - RoomModel.EducationalDialogueCompleted is set to true.
    /// - Completion logic is evaluated.
    /// Throws:
    /// - MissingFieldException if RoomModel is null.
    /// </remarks>
    bool EducationalDialogueCompleted
    {
        get;
        set;
    }

    /// METHODS SECTION

    /// <summary>
    /// Checks whether the room is complete and notifies the view if it is.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomModel must not be null.
    /// - RoomView must not be null.
    /// Postconditions:
    /// - If the room is complete, RoomView.InvokeOnRoomComplete() is called.
    /// Throws:
    /// - MissingFieldException if RoomModel or RoomView is null.
    /// </remarks>
    bool IsComplete();

    /// <summary>
    /// Initializes this controller and validates its dependencies.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized references should be assigned or mocked.
    /// Postconditions:
    /// - Logs errors if RoomView or RoomModel is missing.
    /// - Asserts that both references are valid.
    /// </remarks>
    void Init();
}
