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
    /// Sets the type of the block
    /// </summary>
    /// <param name="blockType">The type of the block</param>
    /// <remarks>
    /// pre-condition:
    ///     - blockType is a valid (string) block type (e.g., )
    /// post-condition:
    ///     - The block's visual appearance is updated to match the specified block type
    /// </remarks>
    void SetBlockType(string blockType);
}
