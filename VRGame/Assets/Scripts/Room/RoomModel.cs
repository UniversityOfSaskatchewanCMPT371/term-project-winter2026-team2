using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model layer for reusable room module.
/// </summary>
public class RoomModel : Model, IRoomModel
{
    /// DATA SECTION

    /// <summary>
    /// Static dictionary for rooms. Used to enforce uniqueness of 
    /// each room by using 'roomId' variable as a key.
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
        /// <summary>
        /// Get the value of Model's 'roomId' variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model's 'roomId' variable must be non-negative.
        /// Postconditions:
        /// - Returns Model's 'roomId' variable.
        /// </remarks>
        get => roomId;
        /// <summary>
        /// Set the value of Model's 'roomId' variable.
        /// </summary>
        /// <remarks
        /// Preconditions:
        /// - 'value' must be non-null.
        /// Postconditions:
        /// - Model's 'roomId' variable set to input 'value'
        /// </remarks>
        set
        {
            if (value < 0)
            {
                Debug.Log("'value' is negative.");
                Debug.Assert(value >= 0, "'value' cannot be negative.");
            }
        }
    }

    /// <inheritdoc/>
    public string Name
    {
        /// <summary>
        /// Get the value of Model's 'roomName' vairable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model's 'roomName' variable must be non-null.
        /// Postconditions:
        /// - Returns Model's 'roomName' variable.
        /// </remarks>
        get => roomName;
        /// <summary>
        /// Set the value of Model's 'roomName' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' must be non-null.
        /// - 'value' cannot be whitespace only.
        /// Postconditions:
        /// - Model's 'roomName' variable set to input 'value'.
        /// </remarks>
        set
        {
            if (value == roomName)
            {
                Debug.Log("'value' is same as current.");
                Debug.Assert(value != roomName, "'value' cannot be the same as current.");
            } else if (value.Trim() == "")
            {
                Debug.Log("'value' is whitespace.");
                Debug.Assert(value.Trim() != "", "'value' cannot be whitespace.");
            }
            roomName = value;
        }
    }

    /// <inheritdoc/>
    public bool MinigameCompleted 
    { 
        /// <summary>
        /// Get the value of Model's 'minigameCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns Model's'minigameCompleted' variable.
        /// </remarks>
        get => minigameCompleted;
        /// <summary>
        /// Set the value of Model's 'minigameCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Model's 'minigameCompleted' variable set to input 'value'.
        /// </remarks>
        set => minigameCompleted = value; 
    }

    /// <inheritdoc/>
    public bool EducationalDialogueCompleted 
    { 
        /// <summary>
        /// Get the value of Model's 'educationalDialogueCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns Model's 'educationalDialogueCompleted' variable.
        /// </remarks>
        /// <returns>
        /// - Returns Model's'educationalDialogueCompleted' variable.
        /// </returns>
        get => educationalDialogueCompleted;
        /// <summary>
        /// Set the value of Model's 'educationalDialogueCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Model's 'educationalDialogueCompleted' variable set to input 'value'.
        /// </remarks>
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
            Debug.LogError("Field 'roomName' cannot be whitespace only.");
        }
        Debug.Assert(roomName.Trim() != "", "Field 'roomName' must be set to a different name.");

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
        bool isKeyTaken = roomLookUp.ContainsKey(Id);
        if (isKeyTaken)
        {
            Debug.LogError("Field 'roomId' is already taken.");
        } else
        {
            // add the key => Model into 'roomLookUp' dictionary.
            roomLookUp.Add(roomId,this);
        }
        Debug.Assert(isKeyTaken == false, "Field 'roomId' must be set to a different id.");

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