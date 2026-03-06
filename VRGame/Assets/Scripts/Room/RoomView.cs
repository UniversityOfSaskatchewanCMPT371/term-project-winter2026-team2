using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// View layer for reusable room module.
/// </summary>
/// <remarks>
/// Requires 'controller component. This class interacts with that component.
/// </remarks>
public class RoomView : View<IRoomController>, IRoomView
{
    /// <summary>
    /// Unity event used to
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