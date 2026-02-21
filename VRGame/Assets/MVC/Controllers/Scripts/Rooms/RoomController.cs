using System;
using System.Data;
using System.Reflection;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.Assertions;

public class RoomController : Controller, IRoomController, InternalRoomController
{
    /// <summary>
    /// Reference to the view layer.
    /// </summary>
    [SerializeField]
    private View roomView;

    /// <summary>
    /// Used to mock view layer.
    /// </summary>
    private IRoomView roomViewMock;

    /// <summary>
    /// Reference to the model layer.
    /// </summary>
    [SerializeField]
    private Model roomModel;

    /// <summary>
    /// Used to mock model layer.
    /// </summary>
    private IRoomModel roomModelMock;

    /// <inheritdoc/>
    public IRoomView RoomView
    {

        get
        {
            if (roomView == null)
            {
                return roomViewMock;
            }
            return (IRoomView)roomView;
        }

        set
        {
            if (value is View viewLayer)
            {
                roomView = viewLayer;
            }
            roomViewMock = value;
        }
    }

    /// <inheritdoc/>
    public IRoomModel RoomModel
    { 

        get
        {
            if (roomModel == null)
            {
                return roomModelMock;
            }
            return (IRoomModel)roomModel;
        }

        set
        {
            if (value is Model modelLayer)
            {
                roomModel = modelLayer;
            }
            roomModelMock = value;
        }
    }

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
            throw new MissingFieldException("Field roomView is missing,");
        }

        if (RoomModel.IsComplete())
        {
            RoomView.InvokeOnRoomComplete();
        }
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
    }

    /// <inheritdoc/>
    void Start()
    {
        Init();
    }
}