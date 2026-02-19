using UnityEngine;
public class RoomModel : MonoBehaviour, IRoomModel, InternalRoomModel
{
    /// <summary>
    /// Unique identifier for this room.
    /// </summary>
    [SerializeField]
    private int roomId = 0;

    /// <summary>
    /// Name of this room.
    /// </summary>
    [SerializeField]
    private string roomName = "RoomNameHere";

    /// <summary>
    /// Completion state of the minigame in this room.
    /// </summary>
    [SerializeField]
    private bool minigameCompleted = false;

    /// <summary>
    /// Completion state of the educational dialogue of this room.
    /// </summary>
    [SerializeField]
    private bool eductionalDialogueCompleted = false;

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
        get => minigameCompleted; 
        set => minigameCompleted = value; 
    }

    /// <summary>
    /// Getter/Setter for this room's educationDialogueCompleted state.
    /// </summary>
    public bool EducationalDialogueCompleted 
    { 
        get => eductionalDialogueCompleted; 
        set => eductionalDialogueCompleted = value; 
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