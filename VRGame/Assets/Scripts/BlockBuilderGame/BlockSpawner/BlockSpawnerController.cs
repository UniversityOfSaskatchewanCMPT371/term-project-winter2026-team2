using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class for BlockSpawner.
/// Manages the logic for spawning bricks in sequence.
/// </summary>
public class BlockSpawnerController : Controller<IBlockSpawnerModel, IBlockSpawnerView>, IBlockSpawnerController
{
    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Start() {}

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// <inheritdoc/>
    public void SpawnBlock()
    {
        GameObject[] prefabs = modelInstance.BrickPrefabs;
        Assert.IsNotNull(prefabs, "BrickPrefabs must not be null");
        Assert.IsTrue(prefabs.Length > 0, "BrickPrefabs must not be empty");

        int index = modelInstance.CurrentBrickIndex;
        Assert.IsTrue(index < prefabs.Length, "CurrentBrickIndex is out of bounds");

        GameObject prefab = prefabs[index];
        modelInstance.CurrentBrickIndex = (index + 1) % prefabs.Length;

        GameObject block = Instantiate(prefab, transform.position, transform.rotation);
        block.transform.localScale = Vector3.one * modelInstance.BrickScale;

        modelInstance.LastSpawnedBrick = block;
    }
}
