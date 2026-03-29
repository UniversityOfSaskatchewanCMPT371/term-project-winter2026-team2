/// <summary>
/// Interface for the TargetBlock Model.
/// Holds the target block and the completion state.
/// </summary>
public interface ITargetBlockModel : IModel
{
    /// <summary>
    /// Initializes the model state.
    /// </summary>
    /// <remarks>
    /// post-condition:
    ///     - ensures IsComplete == false
    /// </remarks>
    new void Init();

    /// <summary>
    /// Accessor method for the completion state of the target block
    /// </summary>
    bool IsComplete 
    { 
        /// <summary>
        /// Returns whether the player's build currently matches the target
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires none
        /// post-condition: 
        ///     - ensures IsComplete is returned
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the completion state of the target block
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires none
        /// post-condition: 
        ///     - ensures IsComplete == value
        /// </remarks>
        set; 
        }
}
