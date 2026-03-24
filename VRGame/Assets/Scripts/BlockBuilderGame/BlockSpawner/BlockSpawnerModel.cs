using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class for BlockSpawner
/// Contains all data related to brick spawning
/// </summary>
public class BlockSpawnerModel : MonoBehaviour, IBlockSpawnerModel
{
    /// <summary>
    /// Array of brick prefabs to cycle through
    /// </summary>
    [SerializeField] private GameObject[] brickPrefabs;

    /// <inheritdoc/>
    public GameObject[] BrickPrefabs
    {
        get
        {
            return brickPrefabs;
        }
        set
        {
            Assert.IsNotNull(value, "BrickPrefabs cannot be null");
            Assert.AreEqual(4, value.Length, "BrickPrefabs array must have exactly 4 elements");
            Debug.Log("Setting BrickPrefabs array with " + value.Length + " elements");
            brickPrefabs = value;
        }
    }


    /// <summary>
    /// Current index in the brick cycle (0-3 since I wanna do 4 different bricks for now)
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

    /// <summary>
    /// Reference to the last spawned brick
    /// </summary>
    [SerializeField] private GameObject lastSpawnedBrick;

    /// <inheritdoc/>
    public GameObject LastSpawnedBrick
    {
        get
        {
            return lastSpawnedBrick;
        }
        set
        {
            Debug.Log("Setting LastSpawnedBrick to " + (value != null ? value.name : "null"));
            lastSpawnedBrick = value;
        }
    }

    /// <inheritdoc/>
    private void Initialize()
    {
        currentBrickIndex = 0;
        spawnHeight = 1.0f;
        brickScale = 4.0f;
        lastSpawnedBrick = null;
    }

}
