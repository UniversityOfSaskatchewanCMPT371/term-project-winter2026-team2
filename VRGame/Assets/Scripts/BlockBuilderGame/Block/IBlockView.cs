using UnityEngine;

public interface IBlockView
{
    /// <summary>
    /// Updates the visual representation of the block
    /// </summary>
    /// <param name="position">The new position of the block</param>
    /// <param name="rotation">The new rotation of the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - The block GameObject has been instantiated and is active in the scene
    /// post-condition:
    ///     - The block's position and rotation are updated to the specified values
    /// </remarks>
    void UpdateVisuals(Vector3 position, Quaternion rotation);


    /// <summary>
    /// Sets the shape of the block
    /// </summary>
    /// <param name="shape">The shape of the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - The block's visual appearance is updated to match the specified block shape
    /// </remarks>
    void SetBlockShape(BlockShape shape);


    /// <summary>
    /// Sets the colour of the block
    /// </summary>
    /// <param name="colour">The colour of the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - The block's visual appearance is updated to match the specified block colour
    /// </remarks>
    void SetBlockColour(BlockColour colour);
}
