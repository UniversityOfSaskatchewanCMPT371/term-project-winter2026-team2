using UnityEngine;
using UnityEngine.Diagnostics;

/// <summary>
/// Controller layer for reusable room module.
/// </summary>
/// </remarks>
/// - Requires view/controller layer to be set before calling Init().
/// <remarks>
public class RoomController : Controller<IRoomModel, IRoomView>, IRoomController
{
    /// METHODS SECTION

    /// <inheritdoc/>
    public void HandleCompleteMinigame()
    {
        CheckModelRef();

        modelInstance.MinigameCompleted = true;

        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompleteEducationalDialogue()
    {
        CheckModelRef();

        modelInstance.EducationalDialogueCompleted = true;

        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompletion()
    {
        CheckModelRef(); 
        CheckViewRef();

        if (modelInstance.IsComplete())
        {
            viewInstance.InvokeOnRoomComplete();
        }

        Debug.Log("Room is not in a valid state of completion.");
    }

    /// <inheritdoc cref="IRoomController.Init"/>
    public override void Init()
    {
        CheckModelRef();
        CheckViewRef();

        Debug.Log("RoomController initialized succefully.");
    }
}