
/// <summary>
/// Default internal interface for room view.
/// </summary>
public interface InternalRoomView
{
    /// DATA SECTION
    
    /// <summary>
    /// Getter/Setter for the controller layer.
    /// Returns the real controller if assigned, otherwise returns the mock.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Getting returns a valid IRoomController (real or mock).
    /// - Setting updates either the real controller reference or the mock.
    /// </remarks>
    public IRoomController RoomController{
        /// <summary>
        /// Access the controller layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the reference to the controller layer.
        /// </remarks>
        /// <returns>
        /// Current completion state of the minigame.
        /// </returns>
        get; 
        /// <summary>
        /// Modify the reference to the controller layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Controller layer cannot be null.
        /// Postconditions:
        /// - None.
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
    /// Called by the controller layer when the room is complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The room is complete.
    /// Postconditions:
    /// - All listeners subscribed to onRoomCompleted are invoked.
    /// </remarks>
    void InvokeOnRoomComplete();

    /// <summary>
    /// Called when the minigame is completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomController is not null.
    /// - The minigame has been completed.
    /// Postconditions:
    /// - The room's minigame completion state is updated through the controller.
    /// </remarks>
    void EducationalDialoguesCompleted();

    /// <summary>
    /// Called when all educational dialogues are complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - RoomController is not null.
    /// - All educational dialogues have been completed.
    /// Postconditions:
    /// - The room's educational dialogue completion state is updated through the controller.
    /// </remarks>
    void MinigameCompleted();

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized fields must be assigned or have valid defaults.
    /// - A reference to the controller layer must be set.
    /// Postconditions:
    /// - Logs errors if RoomController is missing.
    /// - Asserts that RoomController is valid.
    /// </remarks>
    void Init();
}
