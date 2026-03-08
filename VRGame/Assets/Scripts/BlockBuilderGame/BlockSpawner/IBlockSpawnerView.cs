using UnityEngine;

/// <summary>
/// Interface for the BlockSpawner View
/// Handles visual representation and instantiation of bricks
/// </summary>
public interface IBlockSpawnerView
{
    /// <summary>
    /// Instantiates a brick prefab at the specified position
    /// </summary>
    /// <param name="prefab">The brick prefab to instantiate</param>
    /// <param name="position">World position to spawn at</param>
    /// <param name="rotation">Rotation of the spawned brick</param>
    /// <param name="scale">Scale of the spawned brick</param>
    /// <returns>The instantiated GameObject</returns>
    GameObject InstantiateBrick(GameObject prefab, Vector3 position, Quaternion rotation, float scale);

    /// <summary>
    /// Configures the visual properties of a spawned brick
    /// </summary>
    /// <param name="brick">The brick GameObject to configure</param>
    void ConfigureBrickVisuals(GameObject brick);
}
