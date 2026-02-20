using UnityEngine;

/// <summary>
/// Default internal interface for room controller.
/// </summary>
public interface InternalRoomController
{
    // DATA SECTION

    /// <summary>
    /// Internal access to the view layer, 
    /// only accessed via assembly reference to RoomView.
    /// </summary>
    public View RoomView {
        /// <summary>
        /// Access the view layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the reference to the view layer.
        /// </remarks>
        /// <returns>
        /// Current completion state of the minigame.
        /// </returns>
        get; 
        /// <summary>
        /// Modify the reference to the view layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - View layer cannot be null.
        /// Postconditions:
        /// - None.
        /// </remarks>
        set; 
        }

    /// <summary>
    /// Internal access to the model layer, 
    /// only accessed via assembly reference to RoomModel.
    /// </summary>
    public Model RoomModel {
        /// <summary>
        /// Access the model layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the reference to the model layer.
        /// </remarks>
        /// <returns>
        /// Current completion state of the minigame.
        /// </returns>
        get; 
        /// <summary>
        /// Modify the reference to the model layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model layer cannot be null.
        /// Postconditions:
        /// - None.
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
    /// Handles the logic required when the room becomes completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The room is not previously completed.
    /// - All educational dialogue completed.
    /// - Minigame is completed.
    /// Postconditions:
    /// - The room's completion state is updated.
    /// - Any completion-related effects or callbacks have been executed.
    /// </remarks>
    void HandleCompletion();

    /// <summary>
    /// Handles the logic behind changing the state of minigame
    /// completion in the model layer.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - minigame is not complete
    /// Postconditions:
    /// - The room's minigame completion state is updated.
    /// </remarks>
    void HandleCompleteMinigame();

    /// <summary>
    /// Handles the logic behind changing the state of educational
    /// dialogue in the model layer.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Educational dialogue is not complete
    /// Postconditions:
    /// - The room's educational dialogue completion state is updated.
    /// </remarks>
    void HandleCompleteEducationalDialogue();

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Reference to view and model layers must be set.
    /// Postcondtions:
    /// - Asserts that the references to the view and model layers are valid.
    /// - Initializes the model layer.
    /// </remarks>
    void Init();
}
