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
        /// - 'value' must be non-null.
        /// Postconditions:
        /// - Model's 'roomId' variable set to input 'value'
        /// </remarks>
        set;
    }

    /// <summary>
    /// Getter/Setter for this Model's 'roomName' variable.
    /// </summary>
    string Name {
        /// <summary>
        /// Get the value of Model's 'roomName' vairable.
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
        /// - 'value' cannot be whitespace only.
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
        /// <returns>
        /// - Returns Model's'educationalDialogueCompleted' variable.
        /// </returns>
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
    /// Initializes this component and validates each defined variables of its initial values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Model's 'roomId' variable must be unique, and not currently exists as a key in 'roomLookUp' dictionary variable.
    /// - Model's 'roomName' variable cannot be initialized to whitespace only.
    /// - Model's 'minigameCompleted' variable must be initialized to false.
    /// - Model's 'educationalDialogueCompleted' variable must be initialized to false.
    /// Postconditions:
    /// - Adds this Model into 'roomLookUp' dictionary variable with 'roomId' variable as the key.
    /// - Logs errors and assertions if any of the preconditions are violated.
    /// - Component is initialized and each variables of Model is validated.
    /// </remarks>
    new void Init();
}
