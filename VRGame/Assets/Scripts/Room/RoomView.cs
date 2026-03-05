using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class RoomView : View<IRoomController>, IRoomView
{
    /// <summary>
    /// Called by controller when the minigame and 
    /// educational dialogues are completed.
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
        CheckControllerRef();

        controllerInstance.HandleCompleteMinigame();
    }

    /// <inheritdoc/>
    public void EducationalDialoguesCompleted()
    {
        CheckControllerRef();

        controllerInstance.HandleCompleteMinigame();
    }

    /// <inheritdoc cref="IRoomView.Init"/>
    public override void Init()
    {
        CheckControllerRef();

        Debug.Log("RoomView successfully initialized.");
    }
}