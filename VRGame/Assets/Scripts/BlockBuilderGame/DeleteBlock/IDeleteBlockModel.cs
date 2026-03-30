using UnityEngine;
/// <summary>
/// TODO: Change the docstring to match your implementation.
/// </summary>
public interface IDeleteBlockModel : IModel
{
    /// <summary>
    /// TODO: Change the docstring to match your implementation.
    /// </summary>
    new void Init();

    /// <summary>
    /// Accessor for the block's collider component
    /// </summary>
    Collider BlockCollider 
    { 
        /// <summary>
        /// Returns the block's collider component
        /// </summary>
        /// <remarks>
        /// precondition:
        ///     - requires none
        /// postcondition:
        ///     - ensures the block's collider component is returned
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the block's collider component
        /// </summary>
        /// <remarks>
        /// precondition:
        ///     - requires value != null
        /// postcondition:
        ///     - ensures this.collider == value
        /// </remarks>
        set; 
        }
}
