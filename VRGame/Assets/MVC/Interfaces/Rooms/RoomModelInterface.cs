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
    int Id { 
        /// <summary>
        /// Retrieves this room's unique id.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - roomId is not null.
        /// - roomId is non-negative.
        /// Postconditions:
        /// - Returns this room's unique id.
        /// </remarks>
        /// <returns>
        /// This room's unique id.
        /// </returns>
        get;
        /// <summary>
        /// Modifies the value of this room's unique id.
        /// </summary>
        /// <remarks
        /// Preconditions:
        /// - Value is not null.
        /// - Value is non-negative.
        /// Postconditions:
        /// - The value of this room's unique id is modified.
        /// </remarks>
        set;
    }

    /// <summary>
    /// Getter/Setter for this room's name.
    /// </summary>
    string Name {
        /// <summary>
        /// Retrieves the value of this room's name.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not null.
        /// Postconditions:
        /// - Returns this room's name.
        /// </remarks>
        /// <returns>
        /// This room's name.
        /// </returns>
        get;
        /// <summary>
        /// Modifies the value of this room's name.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - value is not null.
        /// - value is non-whitespace.
        /// Postconditions:
        /// - The value of this room's name is modified.
        /// </remarks>
        set;
        }

    /// <summary>
    /// Marks the minigame as complete and triggers completion logic.
    /// </summary>
    bool MinigameCompleted
    {
        /// <summary>
        /// Retrieves the value of minigameCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - minigameCompleted is either true or false.
        /// Postconditions:
        /// - Returns the value of minigameCompleted.
        /// </remarks>
        /// <returns>
        /// The value of minigameCompleted.
        /// </returns>
        get;

        /// <summary>
        /// Modifies the value of minigameCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is either true or false.
        /// Postconditions:
        /// - The value of minigameCompleted is modified.
        /// </remarks>
        set;
    }

    /// <summary>
    /// Marks the educational dialogue as complete and triggers completion logic.
    /// </summary>
    bool EducationalDialogueCompleted
    {
        /// <summary>
        /// Retrieves the value of educationalDialogueCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - educationalDialogueCompleted is either true or false.
        /// Postconditions:
        /// - Returns the value of educationalDialogueCompleted.
        /// </remarks>
        /// <returns>
        /// The value of educationalDialogueCompleted.
        /// </returns>
        get;
        /// <summary>
        /// Modifies the value of educationalDialogueCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is either true or false.
        /// Postconditions:
        /// - The value of educationalDialogueCompleted is modified.
        /// </remarks>
        set;
    }

    /// METHODS SECTION

    /// <summary>
    /// Checks whether the room is in a valid state of completion.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - minigameCompleted value is either true or false.
    /// - educationalDialogue value is either true or false.
    /// Postconditions:
    /// - If the room is complete, RoomView.InvokeOnRoomComplete() is called.
    /// </remarks>
    bool IsComplete();

    /// <summary>
    /// Initializes this model, and validates 
    /// preset values of the data.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized fields should be assigned or have preset values.
    /// - minigameCompleted and educationalDialogueCompleted should be preset to false.
    /// Postconditions:
    /// - Logs errors.
    /// </remarks>
    void Init();
}
