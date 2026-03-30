using UnityEngine;
/// <summary>
/// Interface for the CheckButton Model.
/// Sets up an area container for checking player's built blocks.
/// </summary>
public interface ICheckButtonModel : IModel
{
    /// <summary>
    /// Initializes the model state
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures CheckArea != null
    /// </remarks>
    new void Init();

    /// <summary>
    /// Accessor method for the collider container that defines the area in which built blocks are checked
    /// </summary>
    Collider CheckArea 
    {
        /// <summary>
        /// Returns the collider container that defines the area in which built blocks are checked
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures CheckArea is returned
        /// </remarks>
        get; 
        /// <summary> 
        /// Sets the collider container that defines the area in which built blocks are checked
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires value != null
        /// post-condition:
        ///     - ensures CheckArea == value
        set; 
        }
}
