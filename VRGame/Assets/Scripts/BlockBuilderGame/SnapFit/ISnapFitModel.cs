using UnityEngine;

/// <summary>
/// Interface for the SnapFit Model
/// </summary>
public interface ISnapFitModel : IModel
{
    /// <summary>
    /// Accessor method for the snap point transforms of the block prefab
    /// </summary>
    Transform[] SnapPoints 
    {
        /// <summary>
        /// Returns the snap point transforms of the block prefab
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires none
        /// post-condition: 
        ///     - ensures SnapPoints are returned
        /// </remarks> 
        get; 

        /// <summary>
        /// Sets the snap point transforms of the block prefab
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires value != null
        /// post-condition: 
        ///     - ensures SnapPoints == value
        /// </remarks>
        set; 
    }

    new void Init();
}
