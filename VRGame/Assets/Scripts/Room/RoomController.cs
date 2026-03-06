using UnityEngine;
using UnityEngine.Diagnostics;

/// <summary>
/// Controller layer for reusable room module.
/// </summary>
/// <remarks>
/// Requires 'view' and 'model' components. This class interacts with those components.
/// </remarks>
public class RoomController : Controller<IRoomModel, IRoomView>, IRoomController
{
    /// METHODS SECTION

    /// <inheritdoc/>
    public void HandleCompleteMinigame()
    {
        // Inherited from Controller class. Used to validate 'model' component, 
        // in which this method interacts with.
        CheckModelRef();

        // mark the 'MinigameCompelted' as complete in the 'model' component
        modelInstance.MinigameCompleted = true;

        // See if this room is fully done
        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompleteEducationalDialogue()
    {
        // Inherited from Controller class. Used to validate 'model' component, 
        // in which this method interacts with.
        CheckModelRef();

        // mark the 'EducationalDialogue' as complete in the 'model' component
        modelInstance.EducationalDialogueCompleted = true;

        // See if this room is fully done
        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompletion()
    {
        // These methods are inherited from Controller class. They are
        // used to validate 'model' and 'view' components, in which this method
        // interacts with.
        CheckModelRef(); 
        CheckViewRef();

        // see if the room is fully done. A room is considered done 
        // if 'EducationalDialogueCompleted' and 'MinigameCompleted' fields
        // in the 'model' component are marked as true.
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
        // used to validate 'model' and 'view' components which 'controller'
        // component interacts with.
        CheckModelRef();
        CheckViewRef();

        Debug.Log("RoomController initialized succefully.");
    }
}