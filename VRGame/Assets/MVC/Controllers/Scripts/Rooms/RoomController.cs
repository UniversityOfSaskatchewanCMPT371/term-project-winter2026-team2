using System.Data;
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
    /// Reference to the model layer.
    /// </summary>
    [SerializeField]
    private Model roomModel;

    /// <summary>
    /// Getters/Setters of the view layer.
    /// </summary>
    public View RoomView
    { 
        get => roomView;
        set
        {
            if (value == null)
            {
                throw new NoNullAllowedException();
            }
            roomView = value;
        }
    }

    /// <summary>
    /// Getters/Setters of the model layer.
    /// </summary>
    public Model RoomModel
    { 
        get => roomModel; 
        set
        {
            if (value == null)
            {
                throw new NoNullAllowedException();
            }
            roomModel = value;
        }
    }

    public void MinigameCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void EducationalDialogueCompleted()
    {
        ((IRoomModel)RoomModel).EducationalDialogueCompleted = true;
    }

    public void HandleCompletion()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        if (roomView == null)
        {
            Debug.LogError("Missing field roomView.");
        }
        Assert.IsNotNull(roomView, "Field roomView cannot be null.");

        if (roomView == null)
        {
            Debug.LogError("Misising field roomView.");
        }
        Assert.IsNotNull(roomModel, "Field roomView cannot be null.");
    }
}