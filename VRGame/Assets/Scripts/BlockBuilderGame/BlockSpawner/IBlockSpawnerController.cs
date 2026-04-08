/// <summary>
/// Interface for the BlockSpawner Controller
/// Manages the logic for spawning bricks in sequence
/// </summary>
public interface IBlockSpawnerController : IController
{
    /// <summary>
    /// Initializes the controller and Checks model and view references
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (modelInstance != null) && (viewInstance != null)
    /// post-condition:
    ///     - ensures controller is ready to spawn blocks
    /// </remarks>
    new void Init();

    /// <summary>
    /// Spawns the next brick prefab at this GameObject's transform.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (BrickPrefabs.length > 0) && (0 <= CurrentBrickIndex < BrickPrefabs.Length)
    /// post-conditions:
    ///     - ensures a new block is instantiated at transform.position / transform.rotation,
    ///     - CurrentBrickIndex is advanced to the next cycle position, &&
    ///     - LastSpawnedBrick is updated to the new block
    /// </remarks>
    void SpawnBlock();
}
