using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

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
        // Get the prefabs
        GameObject[] prefabs = modelInstance.BlockPrefabs;
        Assert.IsNotNull(prefabs, "BlockPrefabs must not be null");
        Assert.IsTrue(prefabs.Length > 0, "BlockPrefabs must not be empty");

        // Get the current index of the block to spawn
        int index = modelInstance.CurrentBlockIndex;
        Assert.IsTrue(index < prefabs.Length, "CurrentBlockIndex is out of bounds");

        // If there's a previously spawned block, destroy it before spawning the next one
        XRGrabInteractable lastGrab = null;
        if (modelInstance.LastSpawnedBlock != null)
        {
            lastGrab = modelInstance.LastSpawnedBlock.GetComponent<XRGrabInteractable>();
        }

        // Destroy only if not held
        if (lastGrab != null && !lastGrab.isSelected)
        {
            Destroy(lastGrab.gameObject);
        }

        // Spawn the next block
        GameObject prefab = prefabs[index];
        modelInstance.CurrentBlockIndex = (index + 1) % prefabs.Length;

        // Instantiate the new block at the blockspawner
        GameObject newBlock = Instantiate(prefab, transform.position, transform.rotation);
        newBlock.transform.localScale = Vector3.one * modelInstance.BlockScale;

        // Stop tracking the last spawned block once new one is grabbed
        XRGrabInteractable newGrab = newBlock.GetComponent<XRGrabInteractable>();
        if (newGrab != null)
        {
            newGrab.selectEntered.AddListener(_ => modelInstance.LastSpawnedBlock = null);
        }
        // Update the reference to the last spawned block in the model
        modelInstance.LastSpawnedBlock = newBlock;
    }
}
