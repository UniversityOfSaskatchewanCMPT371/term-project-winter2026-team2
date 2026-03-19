using UnityEngine;

/// <summary>
/// Interface for the BlockSpawner Model
/// Defines the data structure for managing brick spawning
/// </summary>
public interface IBlockSpawnerModel
{
    /// <summary>
    /// Array of brick prefabs available to spawn
    /// </summary>
    BlockShape[] BlocksForPuzzle
    { 
        /// <summary>
        /// Getter for Block shape array
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - BlocksForPuzzle array cannot be null
        /// post-condition:
        ///     - Returns the current array of brick prefabs avialable in level
        /// </remarks>
        get; 
    }


    /// <summary>
    /// Transform where bricks should spawn
    /// </summary>
    Transform SpawnArea 
    {
        /// <summary>
        /// Getter for SpawnArea
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - SpawnArea has been initialized to a valid Transform
        /// post-condition:
        ///     - Returns the Transform indicating the spawn area for bricks
        /// </remarks> 
        get; 

        /// <summary>
        /// Setter for SpawnArea
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value must not be null
        /// post-condition:
        ///     - Sets the SpawnArea to the provided Transform
        /// </remarks>
        set; }


    /// <summary>
    /// Height offset above spawn area point
    /// </summary>
    float SpawnHeight 
    { 
        /// <summary>
        /// Getter for SpawnHeight
        /// </summary>
        /// <remarks>        
        /// pre-condition:
        ///     - SpawnHeight has been initialized to a valid float value
        /// post-condition:
        ///     - Returns the height offset for spawning bricks
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for SpawnHeight
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value can be any float (positive, negative, or zero)
        /// post-condition:
        ///     - Sets the SpawnHeight to the provided value
        /// </remarks>
        set; }


    /// <summary>
    /// Scale multiplier for spawned bricks
    /// </summary>
    float BrickScale 
    {
        /// <summary>
        /// Getter for BrickScale
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - BrickScale has been initialized to a positive float value
        /// post-condition:
        ///     - Returns the scale multiplier for spawned bricks
        /// </remarks> 
        get; 

        /// <summary>
        /// Setter for BrickScale
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value must be greater than 0
        /// post-condition:
        ///     - Sets the BrickScale to the provided value
        /// </remarks>
        set; 
    }


    /// <summary>
    /// Current Current Block Shape Selected in block spawner model
    /// </summary>
    BlockShape CurrentBlockShapeSelected
    { 
        /// <summary>
        /// Getter for CurrentBlockShapeSelected
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - CurrentBlockShapeSelected has been initialized
        /// post-condition:
        ///     - Returns the CurrentBlockShapeSelected selected for brick spawning
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for CurrentBlockShapeSelected
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - input value must not be null
        /// post-condition:
        ///     - Sets the CurrentBlockShapeSelected to the provided value
        /// </remarks>
        set; }



    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentBlockShapeSelected in BlockSpawnerModel is changed
    /// </remarks>
    void SelectNextBlockShape();

    
    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentBlockShapeSelected in BlockSpawnerModel is changed
    /// </remarks>
    void SelectPreviousBlockShape();



    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentBlockShapeSelected in BlockSpawnerModel is changed
    /// </remarks>
    void GetNextBlockShape();

    
    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentBlockShapeSelected in BlockSpawnerModel is changed
    /// </remarks>
    void GetPreviousBlockShape();


    /// <summary>
    /// The colour selected spawned bricks would be
    /// </summary>
    BlockColour CurrentBlockColourSelected
    {
        /// <summary>
        /// Getter for the colour selected for bricks
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Returns the colour for spawned bricks
        /// </remarks> 
        get; 

        /// <summary>
        /// Setter for colour
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Sets the ColourSelected to the provided value
        /// </remarks>
        set; 
    }


    /// <summary>
    /// Selects the next colour for bricks in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentColourSelected in BlockSpawnerModel is changed to next colour in cycle
    /// </remarks>
    void SelectNextColour();

    
    /// <summary>
    /// Selects the previous colour for bricks in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentColourSelected in BlockSpawnerModel is changed to previous colour
    /// </remarks>
    void SelectPreviousColour();



    /// <summary>
    /// Selects the next colour for bricks in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentColourSelected in BlockSpawnerModel is changed to next colour in cycle
    /// </remarks>
    void GetNextColour();

    
    /// <summary>
    /// Selects the previous colour for bricks in the cycle
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - none
    /// post-condition:
    ///     - CurrentColourSelected in BlockSpawnerModel is changed to previous colour
    /// </remarks>
    void GetPreviousColour();


    int CurrentBlockShapeIndex
    {
        /// <summary>
        /// Gets Current index in the block cycle for shapes
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Returns an integer representing the current shape selected for blocks to spawn
        /// </remarks> 
        get; 

        /// <summary>
        /// Gets Current index in the block cycle for shapes
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - currentBlockShapeIndex >= 0
        /// post-condition:
        ///     - current index in block shape cycle is changed
        /// </remarks>
        set; 
    }


    int CurrentBlockColourIndex
    {
        /// <summary>
        /// Getter for the for current colour index
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Returns an integer representing the current colour selected giving blocks.
        /// </remarks> 
        get; 

        /// <summary>
        /// Setter for current colour index
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - currentBlockShapeIndex >= 0
        /// post-condition:
        ///     - Sets the currentBlockColourIndex to the provided value
        /// </remarks>
        set; 
    }
}
