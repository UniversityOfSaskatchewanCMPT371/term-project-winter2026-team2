using UnityEngine;

public interface IBlockController
{
    /// <summary>
    /// Initializes the block with the specified types
    /// </summary>
    /// <param name="shape">The shape of the block to initialize</param>
    /// <param name="colour">The colour of the block to initialize</param>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - The block's model is initialized with the specified BlockShape and BlockColour
    ///     - The block's view is updated to reflect the specified shape and colour.
    /// </remarks>
    void Initialize(BlockShape shape, BlockColour colour);

    /// <summary>
    /// Updates the block's position in the scene
    /// </summary> 
    /// <param name="position">The new position for the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - position is not null and is a valid Vector3
    /// post-condition:
    ///     - The block's model is updated with the new position
    ///     - The block's view is updated to reflect the new position
    /// </remarks>  
    void UpdatePosition(Vector3 position);

    /// <summary>
    /// Updates the block's rotation in the scene
    /// </summary>
    /// <param name="rotation">The new rotation for the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - rotation is not null and is a valid Quaternion
    /// post-condition:
    ///     - The block's model is updated with the new rotation
    ///     - The block's view is updated to reflect the new rotation
    /// </remarks>
    void UpdateRotation(Quaternion rotation);
}
