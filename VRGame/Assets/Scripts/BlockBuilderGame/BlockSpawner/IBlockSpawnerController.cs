using UnityEngine;

/// <summary>
/// Interface for the BlockSpawner Controller
/// Manages the logic for spawning bricks in sequence
/// </summary>
public interface IBlockSpawnerController
{
    /// <summary>
    /// Initializes the spawner with brick prefabs
    /// </summary>
    /// <param name="brick1x1">1x1 brick prefab</param>
    /// <param name="brick1x2">1x2 brick prefab</param>
    /// <param name="brick1x4">1x4 brick prefab</param>
    /// <param name="brick1x6">1x6 brick prefab</param>
    void Initialize(GameObject brick1x1, GameObject brick1x2, GameObject brick1x4, GameObject brick1x6);

    
    /// <summary>
    /// Spawns and already initalized block
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - current selection is not null
    /// post-condition:
    ///     - Returns a GameObject that is a brick prefabs
    /// </remarks>
    GameObject SpawnBlock();


    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    void SelectNextBlock();

    
    /// <summary>
    /// Selects the next brick in the cycle
    /// </summary>
    void SelectPreviousBlock();


    /// <summary>
    /// Gets the current block index
    /// </summary>
    /// <returns>Current Shape in the block cycle</returns>
    BlockShape GetCurrentBlockShape();


    /// <summary>
    /// Gets the current block colour
    /// </summary>
    /// <returns>Current colour in the block colour selector</returns>
    BlockColour GetCurrentBlockColour();
}
