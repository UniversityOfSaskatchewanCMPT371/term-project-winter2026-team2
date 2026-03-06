/// <summary>
/// Interface for RoomController component.
/// </summary>
public interface IRoomController : IController
{
    /// <summary>
    /// Validates Model component by invoking CheckModelRefs() inherited from Controller class. 
    /// Then marks the Model's 'minigameCompleted' variable as complete and invokes HandleCompletion().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// - HandleCompletion() method is implemented.
    /// Postconditions:
    /// - Model's 'minigameCompleted' variable is set to True.
    /// - HandleCompletion() method is invoked.
    /// </remarks>
    void HandleCompleteMinigame();
    
    /// <summary>
    /// Validates Model component by invoking CheckModelRefs() inherited from Controller class. 
    /// Then marks the Model's 'handleCompleteEducationalDialogue' variable as complete and invokes HandleCompletion().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// - HandleCompletion() method is implemented.
    /// Postconditions:
    /// - Model's 'minigameCompleted' variable is set to True.
    /// - HandleCompletion() method is invoked.
    /// </remarks>
    void HandleCompleteEducationalDialogue();

    /// <summary>
    /// Validates the Model and View component by invoking CheckModelRefs() and CheckViewRefs() inherited from Controller class.
    /// Then triggers View's OnRoomComplete event by calling InvokeOnRoomComplete().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is initialized.
    /// - View component is initialized.
    /// Postconditions:
    /// - If Model's IsComplete() reports completion, then View's InvokeOnRoomComplete() is called.
    /// - Logs if the room is not in a state of completion.
    /// </remarks>
    void HandleCompletion();

    /// <summary>
    /// Initializes this component, and invokes CheckModelRef() and CheckViewRef() 
    /// inherited from Controller class to validated Model and View components.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model component is attached to the gameObject (and/or linked to 'inspectorWindowModel' variable).
    /// - View component is attached to the gameObject (and/or linked to 'inspectorWindowView' variable).
    /// Postconditions:
    /// - Logs errors and assertions if Model or View components fail to initialize. Otherwise, logs
    /// succeful initialization of Controller component.
    /// </remarks>
    new void Init();
}
