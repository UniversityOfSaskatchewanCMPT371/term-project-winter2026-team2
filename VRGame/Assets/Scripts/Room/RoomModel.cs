using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;

/// <summary>
/// Model layer for reusable room module.
/// </summary>
public class RoomModel : Model, IRoomModel
{
    /// <summary>
    /// Unique identifier for this room. Preset to Hub so it will never be null
    /// </summary>
    [SerializeField]
    private SceneEnum roomId = SceneEnum.Hub;

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
    public SceneEnum Id
    {
        /// <inheritdoc/>
        get => roomId;
        /// <inheritdoc/>
        set => roomId = value;
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
                Debug.LogError("'value' is exclusively whitespace.");
                Debug.Assert(value.Trim() != "", "'value' cannot be exclusively whitespace.");
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
        // Model's 'roomName' variable cannot be initialized to whitespace exclusively
        if (roomName.Trim() == "")
        {
            Debug.LogError("Field 'roomName' is exclusively whitespace.");
        }
        Debug.Assert(roomName.Trim() != "", "Field 'roomName' cannot be exclusively whitespace.");

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

        Debug.Log("RoomModel successfully initialized.");
    }
}