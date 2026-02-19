using System;
using UnityEngine;
public class RoomView : MonoBehaviour, IRoomView, InternalRoomView
{

    [SerializeField]
    private IRoomController roomController;

    public IRoomView RoomController 
    { 
        get => throw new System.NotImplementedException(); 
        set => throw new System.NotImplementedException(); 
    }

    public event Action OnRoomCompleted;

    public void EducationalDialoguesCompleted()
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
}