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
    /// Spawns the next brick in the cycle
    /// </summary>
    void SpawnNextBrick();

    /// <summary>
    /// Gets the current brick index
    /// </summary>
    /// <returns>Current index in the brick cycle</returns>
    int GetCurrentBrickIndex();
}
