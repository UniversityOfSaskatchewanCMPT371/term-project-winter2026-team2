using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

/// <summary>
/// Model layer for reusable room module.
/// </summary>
public class RoomModel : Model, IRoomModel, InternalRoomModel
{

    /// DATA SECTION

    /// <summary>
    /// Dictionary of rooms available.
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
        /// <summary>
        /// Retrieves this room's unique id.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - roomId is non-negative.
        /// Postconditions:
        /// - Returns this room's unique id.
        /// </remarks>
        get => roomId;
        /// <summary>
        /// Modifies the value of this room's unique id.
        /// </summary>
        /// <remarks
        /// Preconditions:
        /// - value is non-negative.
        /// Postconditions:
        /// - The value of this room's unique id is modified.
        /// </remarks>
        set
        {
            if (value < 0)
            {
                Debug.Log("Value is negative.");
                Debug.Assert(value >= 0, "Value cannot be negative.");
            }
        }
    }

    /// <inheritdoc/>
    public string Name
    {
        /// <summary>
        /// Retrieves the value of this room's name.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is not null.
        /// Postconditions:
        /// - Returns this room's name.
        /// </remarks>
        /// <returns>
        /// This room's name.
        /// </returns>
        get => roomName;
        /// <summary>
        /// Modifies the value of this room's name.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - value is not null.
        /// - value is non-whitespace.
        /// Postconditions:
        /// - The value of this room's name is modified.
        /// </remarks>
        set
        {
            if (value == roomName)
            {
                Debug.Log("Value is same as current.");
                Debug.Assert(value != roomName, "Value cannot be the same as current.");
            } else if (value.Trim() == "")
            {
                Debug.Log("Value is whitespace.");
                Debug.Assert(value.Trim() != "", "Value cannot be whitespace.");
            }
            roomName = value;
        }
    }

    /// <inheritdoc/>
    public bool MinigameCompleted 
    { 
        /// <summary>
        /// Retrieves the value of minigameCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the value of minigameCompleted.
        /// </remarks>
        /// <returns>
        /// The value of minigameCompleted.
        /// </returns>
        get => minigameCompleted;
        /// <summary>
        /// Modifies the value of minigameCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is either true or false.
        /// Postconditions:
        /// - The value of minigameCompleted is modified.
        /// </remarks>
        set => minigameCompleted = value; 
    }

    /// <inheritdoc/>
    public bool EducationalDialogueCompleted 
    { 
        /// <summary>
        /// Retrieves the value of educationalDialogueCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - educationalDialogueCompleted is either true or false.
        /// Postconditions:
        /// - Returns the value of educationalDialogueCompleted.
        /// </remarks>
        /// <returns>
        /// The value of educationalDialogueCompleted.
        /// </returns>
        get => eductionalDialogueCompleted;
        /// Modifies the value of educationalDialogueCompleted.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Value is either true or false.
        /// Postconditions:
        /// - The value of educationalDialogueCompleted is modified.
        /// </remarks>
        set => eductionalDialogueCompleted = value; 
    }

    /// METHODS SECTION

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

    /// <summary>
    /// Called once after all Awake() calls finishes.
    /// Initializes the component by calling Init().
    /// This function is provided by Unity.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Init() function is implemented.
    /// Postconditions:
    /// - Init() function is called.
    /// </remarks>
    void Start()
    {
        Init();
    }

    /// <summary>
    /// Called when the gameObject this 
    /// component is attached to is destroyed.
    /// This function is provided by Unity.
    /// </summary>
    void OnDestroy()
    {
        if (roomLookUp.ContainsKey(Id))
        {
            roomLookUp.Remove(Id);
        }
    }
}