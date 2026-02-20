using System;
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
    /// Called by controller when the minigame and 
    /// educational dialogues are completed.
    /// </summary>
    [SerializeField]
    public UnityEvent onRoomCompleted = new UnityEvent();

    /// <summary>
    /// Getter/Setter for this room's controller layer.
    /// </summary>
    public Controller RoomController 
    { 
        get => roomController; 
        set => roomController = value;
    }

    public void InvokeOnRoomComplete()
    {
       throw new NotImplementedException(); 
    }

    public void Init()
    {
        throw new NotImplementedException();
    }

    public void MinigameCompleted()
    {
        throw new NotImplementedException();
    }

    public void EducationalDialoguesCompleted()
    {
        throw new NotImplementedException();
    }
}