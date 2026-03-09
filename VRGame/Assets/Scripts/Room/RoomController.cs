using UnityEngine;
using UnityEngine.Diagnostics;

/// <summary>
/// Controller layer for reusable room module.
/// </summary>
/// <remarks>
/// Requires View and Model components which this layer interacts with.
/// </remarks>
public class RoomController : Controller<IRoomModel, IRoomView>, IRoomController
{
    /// METHODS SECTION

    /// <inheritdoc/>
    public void HandleCompleteMinigame()
    {
        // see if Model component is initalized
        if (modelInstance == null)
        {
            Debug.LogWarning("Model component not initialized.");
            return;
        }

        // mark the 'MinigameCompleted' as complete in the Model component
        modelInstance.MinigameCompleted = true;

        // See if this room is fully done
        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompleteEducationalDialogue()
    {
        // see if Model component is initalized
        if (modelInstance == null)
        {
            Debug.LogWarning("Model component not initialized.");
            return;
        }

        // mark the 'EducationalDialogue' as complete in the Model component
        modelInstance.EducationalDialogueCompleted = true;

        // See if this room is fully done
        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompletion()
    {
        // see if both Model and View components are initialized
        if (modelInstance == null)
        {
            Debug.LogWarning("Model component not initialized.");
            return;
        } else if (viewInstance == null)
        {
            Debug.LogWarning("View component not initialized.");
        }

        // see if the room is fully done. A room is considered done 
        // if 'EducationalDialogueCompleted' and 'MinigameCompleted' fields
        // in the Model component are marked as true.
        if (modelInstance.IsComplete())
        {
            // 'onRoomCompleted' event field is invoked by calling this method.
            // Any listeners to this event is invoked.
            viewInstance.InvokeOnRoomComplete();
        }

        Debug.Log("Room is not in a valid state of completion.");
    }

    /// <inheritdoc cref="IRoomController.Init"/>
    public override void Init()
    {
        // These methods are inherited from Controller class. They are
        // used to validate Model and View components which 'controller'
        // component interacts with.
        CheckModelRef();
        CheckViewRef();

        Debug.Log("RoomController initialized succefully.");
    }
}