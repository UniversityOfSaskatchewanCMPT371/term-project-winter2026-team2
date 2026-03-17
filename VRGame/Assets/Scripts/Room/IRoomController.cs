/// <summary>
/// Interface for RoomController component.
/// </summary>
public interface IRoomController : IController
{
    /// <summary>
    /// Marks the Model's 'minigameCompleted' variable as complete and invokes HandleCompletion().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// Postconditions:
    /// - Logs errors and assertions if CheckModelRef() fails to validate Model component.
    /// - Model's 'minigameCompleted' variable is set to True.
    /// - HandleCompletion() method is invoked.
    /// </remarks>
    void HandleCompleteMinigame();
    
    /// <summary>
    /// Marks the Model's 'handleCompleteEducationalDialogue' variable as complete and invokes HandleCompletion().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// Postconditions:
    /// - Model's 'educationalDialogueCompleted' variable is set to True.
    /// - HandleCompletion() method is invoked.
    /// </remarks>
    void HandleCompleteEducationalDialogue();

    /// <summary>
    /// Triggers View's OnRoomComplete event by calling InvokeOnRoomComplete() if  the room
    /// is in a state of completion.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// - View component is initialized.
    /// Postconditions:
    /// - If Model's IsComplete() returns true, then View's InvokeOnRoomComplete() is called.
    /// - Logs the current completion status of the room.
    /// </remarks>
    void HandleCompletion();

    /// <summary>
    /// Initializes this component, and invokes CheckModelRef() and CheckViewRef() 
    /// inherited from Controller class to validate Model and View components.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is attached to the gameObject (and/or linked to 'inspectorWindowModel' variable).
    /// - View component is attached to the gameObject (and/or linked to 'inspectorWindowView' variable).
    /// Postconditions:
    /// - Logs errors and assertions if Model or View components fails to initialize. Otherwise, logs
    /// successful initialization of Controller component.
    /// </remarks>
    new void Init();
}
