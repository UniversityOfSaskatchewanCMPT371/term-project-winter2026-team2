using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class RoomView : View, IRoomView, InternalRoomView
{
    /// <summary>
    /// Reference to the controller layer of this room.
    /// </summary>
    [SerializeField]
    private Controller roomController;

    /// <summary>
    /// Used to mock controller layer.
    /// </summary>
    private IRoomController roomControllerMock;

    /// <summary>
    /// Called by controller when the minigame and 
    /// educational dialogues are completed.
    /// </summary>
    public UnityEvent onRoomCompleted = new UnityEvent();

    /// <inheritdoc/>
    public IRoomController RoomController 
    { 
        get
        {
            if (roomController == null)
            {
                return roomControllerMock;
            }
            return (IRoomController)roomController;
        }

        set
        {
            if (value is Controller controllerLayer)
            {
                roomController = controllerLayer;
            }
            roomControllerMock = value;
        }
    }

    /// <inheritdoc/>
    public void InvokeOnRoomComplete()
    {
       onRoomCompleted.Invoke();
    }

    /// <inheritdoc/>
    public void MinigameCompleted()
    {
        if (RoomController == null)
        {
            throw new MissingFieldException("Field roomController is missing.");
        }

        try
        {
            RoomController.HandleCompleteMinigame();
        } catch
        {

        }
    }

    /// <inheritdoc/>
    public void EducationalDialoguesCompleted()
    {
        if (RoomController == null)
        {
            throw new MissingFieldException("Field roomController is missing.");
        }

        try
        {
            RoomController.HandleCompleteMinigame();   
        } catch
        {
            
        }
    }

    /// <inheritdoc/>
    public void Init()
    {
        if (RoomController == null)
        {
            Debug.LogError("Missing field roomController.");
        }
        Debug.Assert(RoomController != null, "Field roomController cannot be null.");
    }

    /// <inheritdoc/>
    void Start()
    {
        Init();
    }
}