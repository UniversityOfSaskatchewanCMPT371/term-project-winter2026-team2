using UnityEngine;

/// <summary>
/// Interface for the BlockSpawner Model
/// Defines the data structure for managing block spawning
/// </summary>
public interface IBlockSpawnerModel : IModel
{
    /// <summary>
    /// Array of block prefabs to spawn
    /// </summary>
    GameObject[] BlockPrefabs 
    { 
        /// <summary>
        /// Getter for BlockPrefabs array
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     -   requires none
        /// post-condition:
        ///     -   ensures the return of the block prefabs array
        /// </remarks>
        get; 

        /// <summary>
        /// Setter for BlockPrefabs array
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     -   requires (value != null) && (value.Length > 0)
        /// post-condition:
        ///     -   ensures BlockPrefabs = value
        /// </remarks>
        set; 
    }

    /// <summary>
    /// Current index in the block cycle
    /// </summary>
    int CurrentBlockIndex 
    { 
        /// <summary>
        /// Getter for CurrentBlockIndex
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures that the current index for block to spawn is returned
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for CurrentBlockIndex
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires value >= 0
        /// post-condition:
        ///     - ensures currentBlockIndex = value
        /// </remarks>
        set; }

    

    /// <summary>
    /// Scale multiplier for spawned blocks
    /// </summary>
    float BlockScale 
    {
        /// <summary>
        /// Getter for BlockScale
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures blockScale is returned
        /// </remarks> 
        get; 

        /// <summary>
        /// Setter for BlockScale
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires value > 0
        /// post-condition:
        ///     - ensures blockScale = value
        /// </remarks>
        set; 
    }

    /// <summary>
    /// Reference to the last spawned block
    /// </summary>
    GameObject LastSpawnedBlock
    {
        /// <summary>
        /// Getter for LastSpawnedBlock
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures lastSpawnedBlock is returned
        /// </remarks>
        get;

        /// <summary>
        /// Setter for LastSpawnedBlock
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - requires value != null
        /// post-condition:
        ///     - ensures lastSpawnedBlock = value
        /// </remarks>
        set;
    }

    /// <summary>
    /// Initializes the currentBlockIndex, blockScale, and lastSpawnedBlock
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures (currentBlockIndex == 0) &&
    ///                 (blockScale == 4.0f) && (lastSpawnedBlock == null)
    new void Init();
}
