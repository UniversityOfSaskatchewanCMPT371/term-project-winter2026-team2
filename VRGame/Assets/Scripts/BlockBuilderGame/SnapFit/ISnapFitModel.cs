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

    /// <summary>
    /// Accessor method for the snap radius of the block prefab
    /// </summary>
    float SnapRadius 
    { 
        /// <summary>
        /// Returns the (float) snap radius of the block prefab
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires none
        /// post-condition: 
        ///     - ensures SnapRadius is returned
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the snap radius of the block prefab
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires value > 0
        /// post-condition: 
        ///     - ensures SnapRadius == value
        /// </remarks>
        set; 
    }

    /// <summary>
    /// Accessor method for whether the block is currently snapped to another block
    /// </summary>
    bool IsSnapped 
    {
        /// <summary>
        /// Returns true if the block is currently snapped to another block, false otherwise
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires none
        /// post-condition: 
        ///     - ensures IsSnapped is returned
        /// </remarks> 
        get; 

        /// <summary>
        /// Sets whether the block is currently snapped to another block
        /// </summary>
        /// <remarks>
        /// pre-condition:  
        ///     - requires value != null
        /// post-condition: 
        ///     - ensures IsSnapped == value
        /// </remarks>
        set; 
    }
    
    
    new void Init();
}
