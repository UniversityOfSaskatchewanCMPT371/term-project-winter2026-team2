using UnityEngine;

/// <summary>
/// Interface for the SpawnButton Model
/// Defines the data structure for managing spawn button state
/// </summary>
public interface ISpawnButtonModel
{
    /// <summary>
    /// Gets or sets the clicked state of the spawn button
    /// </summary>
    bool IsClicked 
    { 
        /// <summary>
        /// Gets the clicked state of the spawn button
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - None
        /// post-condition:
        ///     - Returns the current clicked state of the spawn button
        get; 
        
        /// <summary>
        /// Sets the clicked state of the spawn button
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - value must be a boolean data type
        /// post-condition:
        ///    - Updates the clicked state of the spawn button to the new value
        set; }
}
