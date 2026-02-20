using System;
using System.Data;
using NUnit.Framework;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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

    /// <summary>
    /// Getter/Setter for this room's controller layer.
    /// </summary>
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

    /// <summary>
    /// Called by the controller layer when the room is complete.
    /// </summary>
    /// </remarks>
    /// Preconditions:
    /// - The rooms is complete.
    /// Postconditions:
    /// - Invoke all listeners.
    /// </remarks>
    public void InvokeOnRoomComplete()
    {
       onRoomCompleted.Invoke();
    }

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized fields must be set, or default.
    /// - Reference to controller layer must be set.
    /// Postcondtions:
    /// - Asserts that all Serialized fields are in a valid.
    /// - Asserts that the reference to controller layer is valid.
    /// </remarks>
    public void Init()
    {
        if (RoomController == null)
        {
            Debug.LogError("Missing field roomController.");
        }
        Assert.IsNotNull(RoomController, "Field roomController cannot be null.");
    }

    /// <summary>
    /// Called when the minigame is completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - roomController is not null.
    /// - Minigame is completed.
    /// Postconditions:
    /// - The room's minigame compeltion state is updated.
    /// </remarks>
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

    /// <summary>
    /// Called when all educational dialogues are complete.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - roomController is not null.
    /// - All educational dialogues are completed.
    /// Postconditions:
    /// - The room's educational dialogue completion state is updated.
    /// </remarks>
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

    /// <summary>
    /// Start after all Awake() calls have finished.
    /// Provided/Built-in by Unity.
    /// </summary>
    void Start()
    {
        Init();
    }
}