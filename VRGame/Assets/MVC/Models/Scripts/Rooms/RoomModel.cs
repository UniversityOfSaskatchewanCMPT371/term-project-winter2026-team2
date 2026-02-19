using UnityEngine;
public class RoomModel : MonoBehaviour, IRoomModel, InternalRoomModel
{
    public int roomId = 0;

    public string roomName = "";

    /// <summary>
    /// Getter for this room's unique id.
    /// </summary>
    public int Id => roomId;

    /// <summary>
    /// Getter for this room's name.
    /// </summary>
    public string Name => roomName;

    /// <summary>
    /// Getter/Setter for this room's minigameCompleted state.
    /// </summary>
    public bool MinigameCompleted 
    { 
        get => throw new System.NotImplementedException(); 
        set => throw new System.NotImplementedException(); 
    }

    /// <summary>
    /// Getter/Setter for this room's educationDialogueCompleted state.
    /// </summary>
    public bool EducationalDialogueCompleted 
    { 
        get => throw new System.NotImplementedException(); 
        set => throw new System.NotImplementedException(); 
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public bool IsComplete()
    {
        throw new System.NotImplementedException();
    }
}