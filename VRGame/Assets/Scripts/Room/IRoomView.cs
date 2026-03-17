/// <summary>
/// Interface for RoomView component.
/// </summary>
public interface IRoomView : IView
{
    /// <summary>
    /// Triggers the unity event 'onRoomCompleted' to invoke all listeners subscribed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - All listeners subscribed to the unity event 'onRoomCompleted' are invoked.
    /// </remarks>
    void InvokeOnRoomComplete();

    /// <summary>
    /// Called by the minigame's unity event 'onMinigameFinished', and 
    /// passes onto controller layer to handle minigame completion logic.
    /// to handle logic.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Controller component is initialized.
    /// - Minigame in this room is completed.
    /// Postconditions:
    /// - Invokes the Controller's HandleCompleteMinigame().
    /// </remarks>
    void MinigameCompleted();

    /// <summary>
    /// Called when all educational dialogues are completed, and 
    /// passes onto controller layer to handle education dialogue completion logic.
    /// to handle logic.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Controller component is initialized.
    /// - Educational dialogues in this room is completed.
    /// Postconditions:
    /// - Invokes the Controller's HandleCompleteEducationalDialogue().
    /// </remarks>
    void EducationalDialoguesCompleted();

    /// <summary>
    /// Initializes this component, and invokes CheckControllerRef()
    /// inherited from View class to validate Controller component.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Controller component is attached to the gameObject (and/or linked to 'inspectorWindowController' variable).
    /// Postconditions:
    /// - Logs errors and assertions if Controller component fails to initialize. Otherwise, logs
    /// successful initialization of View component.
    /// </remarks>
    new void Init();
}
