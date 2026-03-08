using UnityEngine;

public interface IBlockController
{
    /// <summary>
    /// Initializes the block with the specified type
    /// </summary>
    /// <param name="blockType">The type of the block to initialize</param>
    /// <remarks>
    /// pre-condition:
    ///     - blockType is a valid (string) block type (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
    /// post-condition:
    ///     - The block's model is initialized with the specified block type
    ///     - The block's view is updated to reflect the specified block type
    /// </remarks>
    void Initialize(string blockType);

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
