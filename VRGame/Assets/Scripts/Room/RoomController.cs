using UnityEngine;
using UnityEngine.Diagnostics;
using System;

/// <summary>
/// Controller layer for reusable room module.
/// </summary>
/// </remarks>
/// - Requires view/controller layer to be set before calling Init().
/// <remarks>
public class RoomController : Controller, IRoomController
{
    /// DATA SECTION

    /// <summary>
    /// Reference to the view layer.
    /// </summary>
    [SerializeField]
    private View roomView;

    /// <summary>
    /// Reference to the mock of view layer component used for testing purposes.
    /// </summary>
    private IRoomView roomViewMock;

    /// <summary>
    /// Reference to the model layer component
    /// </summary>
    [SerializeField]
    private Model roomModel;

    /// <summary>
    /// Reference to the mock of model layer component used for testing purposes.
    /// </summary>
    private IRoomModel roomModelMock;

    /// <inheritdoc/>
    internal IRoomView RoomView
    {
        /// <summary>
        /// Retrieves the view layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remarks>
        /// Precondtion:
        /// - roomView or roomViewMock is not null.
        /// Postcondition:
        /// - returns the reference to view layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to model layer component.
        /// </returns>
        get
        {
            if (roomView == null & roomViewMock == null)
            {
                Debug.Log("Both roomView and roomViewMock fields were null.");
                Debug.Assert(roomView != null | roomViewMock != null, "One of roomView or roomViewMock fields cannot be null.");
            }

            if (roomView == null)
            {
                return roomViewMock;
            }
            return (IRoomView)roomView;
        }
        /// <summary>
        /// Modifies the reference to the view layer component, or the
        /// mock, if the new value inherits from View class.
        /// </summary>
        /// <remarks>
        /// Precondition:
        /// - Value is not null.
        /// - Value is either inherits from View class, or
        /// a mock.
        /// Postcondition:
        /// - Reference to the view layer is modified.
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.Log("Value is null.");
                Debug.Assert(value != null, "Value cannot be null.");
            }

            if (value is View viewLayer)
            {
                roomView = viewLayer;
            }
            roomViewMock = value;
        }
    }

    /// <inheritdoc/>
    internal IRoomModel RoomModel
    { 
        /// <summary>
        /// Retrieves the model layer component, or the
        /// mock, whichever is not null.
        /// </summary>
        /// <remark>
        /// Preconditions:
        /// - roomModel or roomModelMock is not null.
        /// Postconditions:
        /// - returns the reference to model layer component.
        /// </remarks>
        /// <returns>
        /// - The reference to model layer component.
        /// </return>
        get
        {
            if (roomModel == null & roomModelMock == null)
            {
                Debug.Log("Both roomModel and roomModelMock fields were null.");
                Debug.Assert(roomModel != null | roomModelMock != null, "One of roomModel or roomModelMock fields cannot be null.");
            }

            if (roomModel == null)
            {
                return roomModelMock;
            }
            return (IRoomModel)roomModel;
        }
        /// <summary>
        /// Modifies the reference to the model layer component, or the
        /// mock, if the new value inherits from Model class.
        /// </summary>
        /// <remarks>
        /// Precondition:
        /// - Value is not null.
        /// - Value is either inherits from Model class, or
        /// a mock.
        /// Postcondition:
        /// - Reference to the model layer is modified.
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.Log("Value is null.");
                Debug.Assert(value != null, "Value cannot be null.");
            }

            if (value is Model modelLayer)
            {
                roomModel = modelLayer;
            }
            roomModelMock = value;
        }
    }

    /// METHODS SECTION

    /// <inheritdoc/>
    public void HandleCompleteMinigame()
    {
        if (RoomModel == null)
        {
            throw new MissingFieldException("Field roomModel is missing.");
        }
        RoomModel.MinigameCompleted = true;

        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompleteEducationalDialogue()
    {
        if (RoomModel == null)
        {
            throw new MissingFieldException("Field roomModel is missing.");
        }
        RoomModel.EducationalDialogueCompleted = true;

        HandleCompletion();
    }

    /// <inheritdoc/>
    public void HandleCompletion()
    {
        if (RoomModel == null)
        {
            throw new MissingFieldException("Field roomModel is missing.");
        } else if (RoomView == null)
        {
            throw new MissingFieldException("Field roomView is missing.");
        }

        if (RoomModel.IsComplete())
        {
            RoomView.InvokeOnRoomComplete();
        }

        Debug.Log("Room is not in a valid state of completion.");
    }

    /// <inheritdoc/>
    public void Init()
    {
        if (RoomView == null)
        {
            Debug.LogError("Missing field roomView.");
        }
        Debug.Assert(RoomView != null, "Field roomView cannot be null.");

        if (RoomModel == null)
        {
            Debug.LogError("Missing field roomModel.");
        }
        Debug.Assert(RoomModel != null, "Field roomModel cannot be null.");

        Debug.Log("RoomController initialized succefully.");
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