using System;
using NUnit.Framework;
using UnityEditor.PackageManager;
using UnityEngine;
public class RoomModel : Model, IRoomModel, InternalRoomModel
{
    /// <summary>
    /// Unique identifier for this room.
    /// </summary>
    [SerializeField]
    private int roomId;

    /// <summary>
    /// Name of this room.
    /// </summary>
    [SerializeField]
    private string roomName;

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
    /// Getter/Setter for this room's unique id.
    /// </summary>
    public int Id
    {
        /// <summary>
        /// Access the current id of this room.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondition:
        /// - Returns the current id of the room.
        /// </remarks>
        /// <returns>
        /// Current id of the room.
        /// </returns>
        get => roomId;
        /// <summary>
        /// Modify the current value of this room's id.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not the same as previous one
        /// - Value is not null
        /// Postcondition:
        /// - Value of the room's unique id is modified.
        /// </remarks>
        set
        {
            if (value == roomId)
            {
                throw new InvalidOperationException("Value cannot be the same as the current roomId.");
            }
            roomId = value;
        }
    }

    /// <summary>
    /// Getters/Setters for this room's name.
    /// </summary>
    public string Name
    {
        /// <summary>
        /// Access the current name of this room
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondition:
        /// - Returns the current name of the room.
        /// </remarks>
        /// <returns>
        /// Current name of the room.
        /// </returns>
        get => roomName;
        /// <summary>
        /// Modify the current value of this room's name.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not the same as previous one.
        /// - Value is not null.
        /// Postcondition:
        /// - Value of the room's name is modified.
        /// </remarks>
        set
        {
            if (value == roomName)
            {
                throw new InvalidOperationException("Value cannot be the same as the current roomName.");
            }
            roomName = value;
        }
    }

    /// <summary>
    /// Getter/Setter for this room's minigameCompleted state.
    /// </summary>
    public bool MinigameCompleted 
    { 
        /// <summary>
        /// Access the current completion state of minigame.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the current completion state of minigame.
        /// </remarks>
        /// <returns>
        /// Current completion state of the minigame.
        /// </returns>
        get => minigameCompleted;
        /// <summary>
        /// Modifies the completion state of minigame.
        /// </summary>
        /// <remarks>
        /// Precondition:
        /// - Value can either be true or false.
        /// Postcondition:
        /// - The minigame completion state is updated
        /// </remarks>
        set => minigameCompleted = value; 
    }

    /// <summary>
    /// Getter/Setter for this room's educationDialogueCompleted state.
    /// </summary>
    public bool EducationalDialogueCompleted 
    { 
        /// <summary>
        /// Access the current completion state of educational dialogue.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postcondtitions:
        /// - Returns the current completion state of educational dialogue.
        /// </remarks>
        /// <returns>
        /// Current completion state of educational dialogue.
        /// </returns>
        get => eductionalDialogueCompleted;
        /// <summary>
        /// Modifies the completion state of educational dialogue.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value can either be true or false.
        /// Postconditions:
        /// - The educational dialogue completion state is updated.
        /// </remarks>
        set => eductionalDialogueCompleted = value; 
    }

    /// <summary>
    /// Verifies if the room is complete or not.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the current completion state of the room.
    /// </remarks>
    /// <returns>Current completion state of the room.</returns>
    public bool IsComplete()
    {
        return eductionalDialogueCompleted & minigameCompleted;
    }

    /// <summary>
    /// Initializes this component. Called by the game within the MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Serialized fields must be set, or default.
    /// Postcondtions:
    /// - Asserts that all Serialized fields are in a valid.
    /// </remarks>
    public void Init()
    {
        if (roomName != "Hub")
        {
            Debug.LogWarning("Field roomName 'Hub' is reserved.");
        }
        Assert.False(roomName == "Hub", "Field roomId must be set to a different name.");

        if (roomId == 0 && roomName != "Hub")
        {
            Debug.LogWarning("Field roomId '0' is reserved for Hub room.");
        }
        Assert.False(roomId == 0 && roomName != "Hub","Field roomId must be set to a different number.");

        if (roomName.Trim() == "")
        {
            Debug.LogWarning("Field roomName cannot be whitespace.");
        }
        Assert.False(roomName.Trim() == "", "Field roomName must be set to a different name.");

        if (minigameCompleted)
        {
            Debug.LogWarning("Field minigameCompleted must start as false.");
        }
        Assert.False(minigameCompleted, "Field minigameCompleted must be set to false.");

        if (eductionalDialogueCompleted)
        {
            Debug.LogWarning("Field eductionalDialogueCompleted must start as false.");
        }
        Assert.False(eductionalDialogueCompleted, "Field eductionalDialogueCompleted must be set to false.");
    }
}