using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class for BlockSpawner
/// Contains all data related to brick spawning
/// </summary>
public class BlockSpawnerModel : MonoBehaviour, IBlockSpawnerModel
{
    /// <summary>
    /// Array of blockshapes to choose from
    /// </summary>
    [SerializeField] private BlockShape[] blocksAvailable;

    /// <inheritdoc/>
    public BlockShape[] BlocksAvailable
    {
        get
        {
            return blocksAvailable;
        }
        set
        {
            Assert.IsNotNull(value, "blocksAvailable cannot be null");
            Debug.Log("Setting BlocksAvailable array with " + value.Length + " elements");
            blocksAvailable = value;
        }
    }


    /// <summary>
    /// Current index in the brick cycle
    /// </summary>
    [SerializeField] private int currentBrickIndex;

    /// <inheritdoc/>
    public int CurrentBrickIndex
    {
        get
        {
            return currentBrickIndex;
        }
        set
        {
            Assert.IsTrue(value >= 0, "CurrentBrickIndex cannot be negative");
            Debug.Log("Setting CurrentBrickIndex from " + currentBrickIndex + " to " + value);
            currentBrickIndex = value;
        }
    }


    /// <summary>
    /// Current index in the block cycle
    /// </summary>
    public BlockShape CurrentBlockShapeSelected()
    {
        return BlocksAvailable[CurrentBrickIndex];
    }


    /// <summary>
    /// Transform indicating where bricks should spawn
    /// </summary>
    [SerializeField] private Transform spawnArea;

    /// <inheritdoc/>
    public Transform SpawnArea
    {
        get
        {
            return spawnArea;
        }
        set
        {
            Debug.Log("Setting SpawnArea to value to" + value);
            Assert.IsNotNull(value, "SpawnArea cannot be null");
            spawnArea = value;
        }
    }


    /// <summary>
    /// Height offset above spawn point
    /// </summary>
    [SerializeField] private float spawnHeight;


    /// <inheritdoc/>
    public float SpawnHeight
    {
        get
        {
            return spawnHeight;
        }
        set
        {
            Debug.Log("Setting SpawnHeight from " + spawnHeight + " to " + value);
            spawnHeight = value;
        }
    }


    /// <summary>
    /// Scale multiplier for spawned bricks
    /// </summary>
    [SerializeField] private float brickScale;


    /// <inheritdoc/>
    public float BrickScale
    {
        get
        {
            Debug.Log("Getting BrickScale: " + brickScale);
            return brickScale;
        }
        set
        {
            Assert.IsTrue(value > 0, "BrickScale must be greater than 0");
            Debug.Log("Setting BrickScale from " + brickScale + " to " + value);
            brickScale = value;
        }
    }


    /// <inheritdoc/>
    private void Initialize()
    {
        currentBrickIndex = 0;
        spawnHeight = 1.0f;
        brickScale = 4.0f;
    }

    
}
