using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// View layer for reusable room module.
/// </summary>
/// <remarks>
/// Requires Controller component which this layer interacts with.
/// </remarks>
public class RoomView : View<IRoomController>, IRoomView
{
    /// <summary>
    /// Unity event that is triggered by completing the minigame 
    /// and educational dialogue in this room.
    /// </summary>
    public UnityEvent onRoomCompleted = new UnityEvent();

    /// <inheritdoc/>
    public void InvokeOnRoomComplete()
    {
       onRoomCompleted.Invoke();
    }

    /// <inheritdoc/>
    public void MinigameCompleted()
    {
        // see if Controller component is initialized
        if (controllerInstance == null)
        {
            Debug.LogWarning("Controller component not initialized.");
            return;
        }

        controllerInstance.HandleCompleteMinigame();
    }

    /// <inheritdoc/>
    public void EducationalDialoguesCompleted()
    {
        // see if Controller component is initialized
        if (controllerInstance == null)
        {
            Debug.LogWarning("Controller component not initialized.");
            return;
        }

        controllerInstance.HandleCompleteEducationalDialogue();
    }

    /// <inheritdoc cref="IRoomView.Init"/>
    public override void Init()
    {
        // Inherited from View class. Used to validate Controller component, 
        // in which this method interacts with.
        CheckControllerRef();

        Debug.Log("RoomView successfully initialized.");
    }
}