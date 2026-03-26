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
        GameObject[] prefabs = modelInstance.BlockPrefabs;
        Assert.IsNotNull(prefabs, "BlockPrefabs must not be null");
        Assert.IsTrue(prefabs.Length > 0, "BlockPrefabs must not be empty");

        int index = modelInstance.CurrentBlockIndex;
        Assert.IsTrue(index < prefabs.Length, "CurrentBlockIndex is out of bounds");

        if (modelInstance.LastSpawnedBlock != null)
        {
            Destroy(modelInstance.LastSpawnedBlock);
        }

        GameObject prefab = prefabs[index];
        modelInstance.CurrentBlockIndex = (index + 1) % prefabs.Length;

        GameObject block = Instantiate(prefab, transform.position, transform.rotation);
        block.transform.localScale = Vector3.one * modelInstance.BlockScale;

        modelInstance.LastSpawnedBlock = block;
    }
}
