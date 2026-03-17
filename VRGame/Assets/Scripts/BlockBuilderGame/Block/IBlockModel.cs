using UnityEngine;

/// <summary>
/// IBlockModel interface is implemented by the BlockModel class
/// </summary>
public interface IBlockModel
{
    /// <summary>
    /// The accessor and mutator for the block type
    /// </summary>
    BlockShape Shape
    { 
        /// <summary>
        /// The type of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Shape must be of type BlockShape
        /// post-condition:
        ///     - returns the shape of the block
        /// </remarks>
        get;

        /// <summary>
        /// The shape of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
        /// </summary> 
        /// <remarks>
        /// pre-condition:
        ///    - none
        /// post-condition:
        ///    - sets the blockShape to the value
        /// </remarks>
        set; 
    }

    /// <summary>
    /// The accessor and mutator for the block type
    /// </summary>
    BlockColour Colour
    { 
        /// <summary>
        /// The colour of the block (e.g., red, blue, etc.)
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - returns the colour of the block
        /// </remarks>
        get;

        /// <summary>
        /// The colour of the block (e.g., red, blue, etc.)
        /// </summary> 
        /// <remarks>
        /// pre-condition:
        ///    - none
        /// post-condition:
        ///    - sets the colour of the block
        /// </remarks>
        set; 
    }


    /// <summary>
    /// The accessor and mutator method for the position of the block in game
    /// </summary>
    Vector3 TargetPosition 
    { 
        /// <summary>
        /// The desiredPosition of the block in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - position must be a valid Vector3
        /// post-condition:
        ///     - returns the target position of the block in game
        /// </remarks>
        get; 
        
        /// <summary>
        /// The target position of the block in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - value must be a valid Vector3
        /// post-condition:
        ///     - sets the target position of the block in game to the value
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
