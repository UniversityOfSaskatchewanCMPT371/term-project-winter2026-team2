using UnityEngine;

/// <summary>
/// Interface for the BlockSpawner Model
/// Defines the data structure for managing brick spawning
/// </summary>
public interface IBlockSpawnerModel
{
    /// <summary>
    /// Array of brick prefabs to spawn
    /// </summary>
    GameObject[] BrickPrefabs 
    { 
        /// <summary>
        /// Getter for BrickPrefabs array
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - BrickPrefabs array has been initialized with 4 elements
        /// post-condition:
        ///     - Returns the current array of brick prefabs
        /// </remarks>
        get; 

        /// <summary>
        /// Setter for BrickPrefabs array
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input array must not be null
        ///     - Input array must have exactly 4 elements
        /// post-condition:
        ///     - Sets the BrickPrefabs array to the provided value
        /// </remarks>
        set; 
    }

    /// <summary>
    /// Current index in the brick cycle
    /// </summary>
    int CurrentBrickIndex 
    { 
        /// <summary>
        /// Getter for CurrentBrickIndex
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - CurrentBrickIndex has been initialized to a non-negative value
        /// post-condition:
        ///     - Returns the current index for brick spawning
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for CurrentBrickIndex
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value must be non-negative
        /// post-condition:
        ///     - Sets the CurrentBrickIndex to the provided value
        /// </remarks>
        set; }

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
    /// Reference to the last spawned brick
    /// </summary>
    GameObject LastSpawnedBrick
    {
        /// <summary>
        /// Getter for LastSpawnedBrick
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - None (can be null if no brick has been spawned yet)
        /// post-condition:
        ///     - Returns the reference to the last spawned brick GameObject
        /// </remarks>
        get;

        /// <summary>
        /// Setter for LastSpawnedBrick
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value can be null or a valid GameObject reference
        /// post-condition:
        ///     - Sets the LastSpawnedBrick to the provided reference
        /// </remarks>
        set;
    }


    /// <summary>
    /// Selects block to spawn
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - BrickPrefabs array has been initialized with 4 elements
    /// post-condition:
    ///     - Returns the current array of brick prefabs
    /// </remarks>
    public BlockModel SpawnBlock()
    {
        
    }

}
