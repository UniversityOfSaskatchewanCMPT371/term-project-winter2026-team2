using System.Data;
using NUnit.Framework;
using UnityEngine;

public class RoomController : MonoBehaviour, IRoomController, InternalRoomController
{
    /// <summary>
    /// Reference to the view layer.
    /// </summary>
    [SerializeField]
    private IRoomView roomView;

    /// <summary>
    /// Reference to the model layer.
    /// </summary>
    [SerializeField]
    private IRoomModel roomModel;

    /// <summary>
    /// Getters/Setters of the view layer.
    /// </summary>
    public IRoomView RoomView 
    { 
        get => throw new System.NotImplementedException(); 
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
    public IRoomModel RoomModel 
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