using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.BranchExplorer;
using NUnit.Framework;
using UnityEditor.PackageManager;
using UnityEngine;
public class RoomModel : Model, IRoomModel, InternalRoomModel
{
    /// <summary>
    /// Dictionary of rooms already created.
    /// </summary>
    private static Dictionary<int, RoomModel> roomLookUp = new Dictionary<int, RoomModel>();

    /// <summary>
    /// Unique identifier for this room.
    /// </summary>
    [SerializeField]
    private int roomId = 0;

    /// <summary>
    /// Name of this room.
    /// </summary>
    [SerializeField]
    private string roomName = "Room";

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

    /// <inheritdoc/>
    public int Id
    {
        get => roomId;
        set => roomId = value;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public bool MinigameCompleted 
    { 
        get => minigameCompleted;
        set => minigameCompleted = value; 
    }

    /// <inheritdoc/>
    public bool EducationalDialogueCompleted 
    { 
        get => eductionalDialogueCompleted;
        set => eductionalDialogueCompleted = value; 
    }

    /// <inheritdoc/>
    public bool IsComplete()
    {
        return eductionalDialogueCompleted & minigameCompleted;
    }

    /// <inheritdoc/>
    public void Init()
    {
        if (roomName.Trim() == "")
        {
            Debug.LogError("Field roomName cannot be whitespace.");
        }
        Debug.Assert(roomName.Trim() != "", "Field roomName must be set to a different name.");

        if (minigameCompleted)
        {
            Debug.LogError("Field minigameCompleted must start as false.");
        }
        Debug.Assert(minigameCompleted == false, "Field minigameCompleted must be set to false.");

        if (eductionalDialogueCompleted)
        {
            Debug.LogError("Field eductionalDialogueCompleted must start as false.");
        }
        Debug.Assert(eductionalDialogueCompleted == false, "Field eductionalDialogueCompleted must be set to false.");

        bool isKeyTaken = roomLookUp.ContainsKey(Id);
        if (isKeyTaken)
        {
            Debug.LogError("Field roomId is already taken.");
        } else
        {
            roomLookUp.Add(roomId,this);
        }
        Debug.Assert(isKeyTaken == false, "Field roomId must be set to a different id.");
    }

    /// <inheritdoc/>
    void Start()
    {
        Init();
    }

    /// <inheritdoc/>
    void OnDestroy()
    {
        if (roomLookUp.ContainsKey(Id))
        {
            roomLookUp.Remove(Id);
        }
    }
}