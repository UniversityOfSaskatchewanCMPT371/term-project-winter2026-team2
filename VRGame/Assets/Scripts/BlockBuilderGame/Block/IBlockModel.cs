using UnityEngine;

/// <summary>
/// IBlockModel interface is implemented by the BlockModel class
/// </summary>
public interface IBlockModel
{
    /// <summary>
    /// The accessor and mutator for the block type
    /// </summary>
    string BlockType 
    { 
        /// <summary>
        /// The type of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - blockType must be a valid block of type string
        /// post-condition:
        ///     - returns the type of the block
        /// </remarks>
        get;

        /// <summary>
        /// The type of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
        /// </summary> 
        /// <remarks>
        /// pre-condition:
        ///    - value must be a valid block of type string
        /// post-condition:
        ///    - sets the type of the block to the value
        /// </remarks>
        set; 
    
    }

    
    /// <summary>
    /// The accessor and mutator method for the position of the block in game
    /// </summary>
    Vector3 Position 
    { 
        /// <summary>
        /// The position of the block in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - position must be a valid Vector3
        /// post-condition:
        ///     - returns the position of the block in game
        /// </remarks>
        get; 
        
        /// <summary>
        /// The position of the block in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - value must be a valid Vector3
        /// post-condition:
        ///     - sets the position of the block in game to the value
        /// </remarks>
        set; 
    }


    /// <summary>
    /// The accessor and mutator method for the rotation of the block in game
    /// </summary> 
    Quaternion Rotation 
    { 
        /// <summary>
        /// The rotation of the block in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - rotation must be a valid Quaternion
        /// post-condition:
        ///     - returns the rotation of the block in game
        /// </remarks>
        get; 

        /// <summary>
        /// The rotation of the block in game
        /// </summary> 
        /// <remarks>
        /// pre-condition:
        ///     - value must be a valid Quaternion
        /// post-condition:
        ///     - sets the rotation of the block in game to the value
        /// </remarks>
        set; 
    }
}
