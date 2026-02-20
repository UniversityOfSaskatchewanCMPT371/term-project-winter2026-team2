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

    /// <summary>
    /// Getters/Setters of the view layer.
    /// </summary>
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

    /// <summary>
    /// Getters/Setters of the model layer.
    /// </summary>
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

    /// <summary>
    /// Handles the logic behind changing the state of minigame
    /// completion in the model layer.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - roomModel is not null.
    /// - minigame is not complete.
    /// Postconditions:
    /// - The room's minigame completion state is updated.
    /// </remarks>
    /// <throws>
    public void HandleCompleteMinigame()
    {
        if (RoomModel == null)
        {
            throw new MissingFieldException("Field roomModel is missing.");
        }
        RoomModel.MinigameCompleted = true;
    }

    /// <summary>
    /// Handles the logic behind changing the state of educational
    /// dialogue in the model layer.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - roomModel is not null.
    /// - Educational dialogue is not complete
    /// Postconditions:
    /// - The room's educational dialogue completion state is updated.
    /// </remarks>
    public void HandleCompleteEducationalDialogue()
    {
        if (RoomModel == null)
        {
            throw new MissingFieldException("Field roomModel is missing.");
        }
        RoomModel.EducationalDialogueCompleted = true;
    }

    /// <summary>
    /// Handles the logic required when the room becomes completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - roomModel is not null.
    /// - roomView is not null.
    /// - The room is not previously completed.
    /// - All educational dialogue completed.
    /// - Minigame is completed.
    /// Postconditions:
    /// - The room's completion state is updated.
    /// - Any completion-related effects or callbacks have been executed.
    /// </remarks>
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

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Reference to view and model layers must be set.
    /// Postcondtions:
    /// - Asserts that the references to the view and model layers are valid.
    /// - Initializes the model layer.
    /// </remarks>
    public void Init()
    {
        if (RoomView == null)
        {
            Debug.LogError("Missing field roomView.");
        }
        Assert.IsNotNull(RoomView, "Field roomView cannot be null.");

        if (RoomModel == null)
        {
            Debug.LogError("Misising field roomModel.");
        }
        Assert.IsNotNull(RoomModel, "Field roomModel cannot be null.");

        RoomModel.Init();
    }
}