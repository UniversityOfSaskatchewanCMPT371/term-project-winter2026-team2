using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Model layer for reusable room module.
/// </summary>
public class RoomModel : Model, IRoomModel
{
    /// DATA SECTION

    /// <summary>
    /// Static dictionary for rooms.
    /// </summary>
    /// <remarks>
    /// int       : A Model's 'roomId' variable is used as a key to look up its Model.
    /// RoomModel : The Model that is mapped to its 'roomId' variable as a key.
    /// </remarks>
    private static Dictionary<int, RoomModel> roomLookUp = new Dictionary<int, RoomModel>();

    /// <summary>
    /// Unique identifier for this room. Used as a key in 'roomLookUp' dictionary 
    /// variable which maps to this Model.
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
    private bool educationalDialogueCompleted = false;

    /// <inheritdoc/>
    public int Id
    {
        /// <inheritdoc/>
        get
        {
            if (roomId < 0)
            {
                Debug.LogError("Field 'roomId' is negative.");
                Debug.Assert(roomId >= 0, "Field 'roomId' must be non-negative.");
            }

            return roomId;
        }
        /// <inheritdoc/>
        set
        {
            if (value < 0)
            {
                Debug.LogError("'value' is negative.");
                Debug.Assert(value >= 0, "'value' must be non-negative.");
            }

            if (roomLookUp.ContainsKey(value))
            {
                Debug.LogError("'value' is already taken.");
                Debug.Assert(roomLookUp.ContainsKey(value) == false, "'value' must be unique.");
            }

            roomId = value;
        }
    }

    /// <inheritdoc/>
    public string Name
    {
        /// <inheritdoc/>
        get
        {
            if (roomName == null)
            {
                Debug.LogError("Field 'roomName' is null.");
                Debug.Assert(roomName != null, "Field 'roomName' cannot be null.");
            }

            return roomName;
        }
        /// <inheritdoc/>
        set
        {
            if (value == null)
            {
                Debug.LogError("'value' is null.");
                Debug.Assert(value != null, "'value' cannot be null.");
            } else if (value == roomName)
            {
                Debug.LogError("'value' is same as current.");
                Debug.Assert(value != roomName, "'value' cannot be the same as current.");
            } else if (value.Trim() == "")
            {
                Debug.LogError("'value' is only whitespace.");
                Debug.Assert(value.Trim() != "", "'value' cannot be whitespace only.");
            }
            roomName = value;
        }
    }

    /// <inheritdoc/>
    public bool MinigameCompleted 
    { 
        /// <inheritdoc/>
        get => minigameCompleted;
        /// <inheritdoc/>
        set => minigameCompleted = value; 
    }

    /// <inheritdoc/>
    public bool EducationalDialogueCompleted 
    { 
        /// <inheritdoc/>
        get => educationalDialogueCompleted;
        /// <inheritdoc/>
        set => educationalDialogueCompleted = value; 
    }

    /// METHODS SECTION

    /// <inheritdoc/>
    public bool IsComplete()
    {
        return educationalDialogueCompleted & minigameCompleted;
    }

    /// <inheritdoc cref="IRoomModel.Init"/>
    public override void Init()
    {
        // Model's 'roomName' variable cannot be initialized to whitespace only
        if (roomName.Trim() == "")
        {
            Debug.LogError("Field 'roomName' is only whitespace.");
        }
        Debug.Assert(roomName.Trim() != "", "Field 'roomName' cannot be only whitespace.");

        // Model's 'roomId' variable cannot be initialized to negative integer
        if (roomId < 0)
        {
            Debug.LogError("Field 'roomId' is negative.");
        }
        Debug.Assert(roomId >= 0, "Field 'roomId' must be non-negative.");

        // Model's 'minigameCompleted' variable must be initialized to false
        if (minigameCompleted)
        {
            Debug.LogError("Field 'minigameCompleted' must start as false.");
        }
        Debug.Assert(minigameCompleted == false, "Field 'minigameCompleted' must be set to false.");

        // Model's 'educationalDialogueCompleted' variable must be initialized to false
        if (educationalDialogueCompleted)
        {
            Debug.LogError("Field 'eductionalDialogueCompleted' must start as false.");
        }
        Debug.Assert(educationalDialogueCompleted == false, "Field 'eductionalDialogueCompleted' must be set to false.");

        // see if this Model's 'roomId' variable already exists in the 'roomLookUp' dictionary.
        // This enforces each room Model to have a unique 'roomId'.
        bool isKeyTaken = roomLookUp.ContainsKey(roomId);
        if (isKeyTaken)
        {
            Debug.LogError("Field 'roomId' is already taken.");
        } else
        {
            // add the key => Model into 'roomLookUp' dictionary.
            roomLookUp.Add(roomId, this);
        }
        Debug.Assert(isKeyTaken == false, "Field 'roomId' must be unique.");

        Debug.Log("RoomModel successfully initialized.");
    }

    /// <summary>
    /// Called when the gameObject this
    /// component is attached to is destroyed.
    /// This function is provided by Unity.
    /// </summary>
    /// <remarks>
    /// This method is called by DestroyImmediate(this.gameObject) or Destroy(this.gameObject) which is used
    /// in testing. Otherwise calling Init() in testing would result in failure since each Model's 'roomId' variable is preset to 0.
    /// </remarks>
    void OnDestroy()
    {
        if (roomLookUp.ContainsKey(Id))
        {
            roomLookUp.Remove(Id);
        }
    }
}