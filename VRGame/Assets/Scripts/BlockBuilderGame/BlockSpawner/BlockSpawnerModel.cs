using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class for BlockSpawner
/// Contains all data related to block spawning
/// </summary>
public class BlockSpawnerModel : Model, IBlockSpawnerModel
{
    /// <summary>
    /// Array of block prefabs to cycle through
    /// </summary>
    [SerializeField] private GameObject[] blockPrefabs;

    /// <inheritdoc/>
    public GameObject[] BlockPrefabs
    {
        get
        {
            return blockPrefabs;
        }
        set
        {
            Assert.IsNotNull(value, "value to set for blockPrefabs must not be null");
            Assert.IsTrue(value.Length > 0, "blockPrefabs array must have at least 1 element");
            blockPrefabs = value;
        }
    }


    /// <summary>
    /// Current index in the block cycle (0-3 since I wanna do 4 different blocks for now)
    /// </summary>
    private int currentBlockIndex;

    /// <inheritdoc/>
    public int CurrentBlockIndex
    {
        get
        {
            return currentBlockIndex;
        }
        set
        {
            Assert.IsTrue(value >= 0, "CurrentblockIndex cannot be negative");
            currentBlockIndex = value;
        }
    }


    /// <summary>
    /// Scale multiplier for spawned blocks
    /// </summary>
    private float blockScale;


    /// <inheritdoc/>
    public float BlockScale
    {
        get
        {
            return blockScale;
        }
        set
        {
            Assert.IsTrue(value > 0, "blockScale must be greater than 0");
            blockScale = value;
        }
    }

    /// <summary>
    /// Reference to the last spawned block
    /// </summary>
    private GameObject lastSpawnedBlock;

    /// <inheritdoc/>
    public GameObject LastSpawnedBlock
    {
        get
        {
            return lastSpawnedBlock;
        }
        set
        {
            lastSpawnedBlock = value;
            // No assertions due to lastSpawnedBlock will be null on next block spawn
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {
        currentBlockIndex = 0;
        blockScale = 1.0f;
        lastSpawnedBlock = null;

        if (currentBlockIndex != 0)
        {
            Debug.LogError("Failed to set currentBlockIndex to 0 on BlockSpawnerModel Init()")
        }
        Assert.AreEqual(currentBlockIndex, 0, "currentBlockIndex failed to set on Init()");

        if (blockScale != 1.0f)
        {
            Debug.LogError("Failed to set blockScale to 1.0f on BlockSpawnerModel Init()")
        }
        Assert.AreEqual(blockScale, 1.0f, "blockScale failed to set on Init()");

        if (lastSpawnedBlock != null)
        {
            Debug.LogError("Failed to set lastSpawnedBlock to null on BlockSpawnerModel Init()")
        }
        Assert.IsNull(lastSpawnedBlock, "lastSpawnedBlock failed to set on Init()");
    }

}
