using System.Data;
using UnityEngine;
using UnityEngine.Assertions;

public class RoomController : Controller, IRoomController, InternalRoomController
{
    /// <summary>
    /// Reference to the view layer.
    /// </summary>
    [SerializeField]
    private View roomView;

    /// <summary>
    /// Reference to the model layer.
    /// </summary>
    [SerializeField]
    private Model roomModel;

    /// <summary>
    /// Getters/Setters of the view layer.
    /// </summary>
    public View RoomView
    { 
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
        get => roomView;
        /// <summary>
        /// Modify the reference to the view layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - View layer cannot be null.
        /// Postconditions:
        /// - None.
        /// </remarks>
        set
        {
            if (value == null)
            {
                throw new NoNullAllowedException();
            }
            roomView = value;
        }
    }

    /// <summary>
    /// Getters/Setters of the model layer.
    /// </summary>
    public Model RoomModel
    { 
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
        get => roomModel; 
        /// <summary>
        /// Modify the reference to the model layer of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model layer cannot be null.
        /// Postconditions:
        /// - None.
        /// </remarks>
        set
        {
            if (value == null)
            {
                throw new NoNullAllowedException();
            }
            roomModel = value;
        }
    }

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
    public void HandleCompleteMinigame()
    {
        ((IRoomModel)RoomModel).MinigameCompleted = true;
    }

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
    public void HandleCompleteEducationalDialogue()
    {
        ((IRoomModel)RoomModel).EducationalDialogueCompleted = true;
    }

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
    public void HandleCompletion()
    {
        if (((IRoomModel)RoomModel).IsComplete())
        {
            ((IRoomView)RoomView).InvokeOnRoomComplete();
        }
    }

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
    public void Init()
    {
        if (roomView == null)
        {
            Debug.LogError("Missing field roomView.");
        }
        Assert.IsNotNull(roomView, "Field roomView cannot be null.");

        if (roomView == null)
        {
            Debug.LogError("Misising field roomView.");
        }
        Assert.IsNotNull(roomModel, "Field roomView cannot be null.");

        ((IRoomModel)roomModel).Init();
    }
}