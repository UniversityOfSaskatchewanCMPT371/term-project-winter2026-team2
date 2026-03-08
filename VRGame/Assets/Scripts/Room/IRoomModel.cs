/// <summary>
/// Interface for RoomModel component.
/// </summary>
public interface IRoomModel : IModel
{
    /// DATA SECTION

    /// <summary>
    /// Getter/Setter for this Model's 'roomId' variable.
    /// </summary>
    int Id { 
        /// <summary>
        /// Get the value of Model's 'roomId' variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model's 'roomId' variable must be non-negative.
        /// Postconditions:
        /// - Returns Model's 'roomId' variable.
        /// </remarks>
        get;
        /// <summary>
        /// Set the value of Model's 'roomId' variable.
        /// </summary>
        /// <remarks
        /// Preconditions:
        /// - 'value' must be non-negative.
        /// - 'value' must not exist in 'roomLookUp' dictionary.
        /// Postconditions:
        /// - Certifies that the 'value' is unique. If so, then the
        /// Model's 'roomId' variable set to input 'value'
        /// </remarks>
        set;
    }

    /// <summary>
    /// Getter/Setter for this Model's 'roomName' variable.
    /// </summary>
    string Name {
        /// <summary>
        /// Get the value of Model's 'roomName' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - Model's 'roomName' variable must be non-null.
        /// Postconditions:
        /// - Returns Model's 'roomName' variable.
        /// </remarks>
        get;
        /// <summary>
        /// Set the value of Model's 'roomName' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' must be non-null.
        /// - 'value' is not the same as current.
        /// - 'value' is not exclusively whitespace.
        /// Postconditions:
        /// - Model's 'roomName' variable set to input 'value'.
        /// </remarks>
        set;
        }

    /// <summary>
    /// Getter/Setter for this Model's 'minigameCompleted' variable.
    /// </summary>
    bool MinigameCompleted
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
        get;
        /// <summary>
        /// Set the value of Model's 'minigameCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Model's 'minigameCompleted' variable set to input 'value'.
        /// </remarks>
        set;
    }

    /// <summary>
    /// Getter/Setter for this Model's 'educationalDialogueCompleted' variable.
    /// </summary>
    bool EducationalDialogueCompleted
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
        get;
        /// <summary>
        /// Set the value of Model's 'educationalDialogueCompleted' variable.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Model's 'educationalDialogueCompleted' variable set to input 'value'.
        /// </remarks>
        set;
    }

    /// METHODS SECTION

    /// <summary>
    /// Checks whether the room's Model is in a valid state of completion.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns True when the Model's 'minigameCompleted' and 'educationalDialogueCompleted' variables
    /// are both True. Returns False otherwise.
    /// </remarks>
    bool IsComplete();

    /// <summary>
    /// Initializes this component and validates initial values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model's 'roomName' variable is not null or exclusively whitespace.
    /// - Model's 'roomId' variable is non-negative and unique in 'roomLookUp' dictionary.
    /// - Model's 'minigameCompleted' variable are false.
    /// - Model's 'educationalDialogueCompleted' variable are false.
    /// Postconditions:
    /// - Component is initialized and each variables are validated.
    /// - Adds this Model into 'roomLookUp' dictionary variable with 'roomId' variable as the key.
    /// - Logs errors and assertions if any of the preconditions are violated, otherwise logs successful initialization.
    /// </remarks>
    new void Init();

    /// <summary>
    /// Called when the gameObject this
    /// component is attached to is destroyed.
    /// This function is provided by Unity.
    /// </summary>
    /// <remarks>
    /// This method is called by DestroyImmediate(this.gameObject) or Destroy(this.gameObject) which is used
    /// in testing. Otherwise calling Init() in testing would result in failure since each Model's 'roomId' variable is preset to 0.
    /// </remarks>
    void OnDestroy();
}
