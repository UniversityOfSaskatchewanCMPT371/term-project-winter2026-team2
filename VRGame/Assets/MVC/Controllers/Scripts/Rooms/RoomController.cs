using System.Data;
using UnityEngine;

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
        get => throw new System.NotImplementedException(); 
        set => roomView = value;
    }

    /// <summary>
    /// Getters/Setters of the model layer.
    /// </summary>
    public Model RoomModel 
    { 
        get => throw new System.NotImplementedException(); 
        set
        {
            if (value == null)
            {
                throw new NoNullAllowedException();
            }
            roomModel = value;
        }
    }

    public void HandleCompletion()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }
}