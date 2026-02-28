using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class RoomView : View, IRoomView
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
    internal IRoomController RoomController 
    { 
        /// <summary>
        /// Retrieves the controller layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - roomController or roomControllerMock is not null.
        /// Postconditions:
        /// - Returns the reference to controller layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to the controller layer component.
        /// </returns>
        get
        {
            if (roomController == null & roomControllerMock == null)
            {
                Debug.Log("Both roomController and roomControllerMock fields were null.");
                Debug.Assert(roomController != null | roomControllerMock != null, "One of roomController or roomControllerMock fields cannot be null.");
            }

            if (roomController == null)
            {
                return roomControllerMock;
            }
            return (IRoomController)roomController;
        }
        /// <summary>
        /// Modifies the reference to the controller layer component, or the
        /// mock, if the new value inherits from Controller class.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not null
        /// - Value either inherits from Controller class, or a mock.
        /// Postconditions:
        /// - Reference to the controller layer is modified.
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.Log("Value is null.");
                Debug.Assert(value != null, "Value cannot be null.");
            }

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

    /// <summary>
    /// Called once after all Awake() calls finishes.
    /// Initializes the component by calling Init().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Init() function is implemented.
    /// Postconditions:
    /// - Init() function is called.
    /// </remarks>
    void Start()
    {
        Init();
    }
}