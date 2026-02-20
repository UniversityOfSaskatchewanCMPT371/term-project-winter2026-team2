
/// <summary>
/// Default internal interface for room view.
/// </summary>
public interface InternalRoomView
{
    /// DATA SECTION
    
    /// <summary>
    /// Access to the controller layer, 
    /// </summary>
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
    /// </remarks>
    /// Preconditions:
    /// - The rooms is complete.
    /// Postconditions:
    /// - Invoke all listeners.
    /// </remarks>
    void InvokeOnRoomComplete();

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
