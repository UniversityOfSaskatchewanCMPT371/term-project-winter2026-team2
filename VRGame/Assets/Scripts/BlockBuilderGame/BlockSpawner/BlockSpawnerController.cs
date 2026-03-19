using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class for BlockSpawner
/// Manages the logic for spawning bricks in sequence
/// </summary>
public class BlockSpawnerController : MonoBehaviour, IBlockSpawnerController
{
    /// <summary>
    /// Instance of the Model and View components
    /// </summary>
    private IBlockSpawnerModel model;
    private IBlockSpawnerView view;


    private void Awake()
    {
        // Get or add Model component
        model = GetComponent<IBlockSpawnerModel>();
        if (model == null)
        {
            model = gameObject.AddComponent<BlockSpawnerModel>();
        }

        // Get or add View component
        view = GetComponent<IBlockSpawnerView>();
        if (view == null)
        {
            view = gameObject.AddComponent<BlockSpawnerView>();
        }

        Assert.IsNotNull(model, "Model is null after initialization!");
        Assert.IsNotNull(view, "View is null after initialization!");
    }

    private void Start()
    {
        // Load brick prefabs from Kenny's Brick Kit 
        /*
        if (brick1x1Prefab == null)
        {
            brick1x1Prefab = Resources.Load<GameObject>("FreeAssets/KennyBrickKit/bevel_hq_brick_1x1");
            Debug.Log("[BlockSpawnerController] Loaded brick1x1Prefab from Resources");
        }
        if (brick1x2Prefab == null)
        {
            brick1x2Prefab = Resources.Load<GameObject>("FreeAssets/KennyBrickKit/bevel_hq_brick_1x2");
            Debug.Log("[BlockSpawnerController] Loaded brick1x2Prefab from Resources");
        }
        if (brick1x4Prefab == null)
        {
            brick1x4Prefab = Resources.Load<GameObject>("FreeAssets/KennyBrickKit/bevel_hq_brick_1x4");
            Debug.Log("[BlockSpawnerController] Loaded brick1x4Prefab from Resources");
        }
        if (brick1x6Prefab == null)
        {
            brick1x6Prefab = Resources.Load<GameObject>("FreeAssets/KennyBrickKit/bevel_hq_brick_1x6");
            Debug.Log("[BlockSpawnerController] Loaded brick1x6Prefab from Resources");
        }

        // Initialize the controller
        Initialize(brick1x1Prefab, brick1x2Prefab, brick1x4Prefab, brick1x6Prefab);

        // Set spawn area point to this object's position
        if (spawnArea == null)
        {
            spawnArea = transform;
            Debug.Log("spawnArea set to self transform");
            Assert.IsNotNull(spawnArea, "spawnArea is null after setting to self transform!");
        }

        model.SpawnArea = spawnArea;
        model.SpawnHeight = spawnHeight;
        model.BrickScale = brickScale;

        Debug.Log("[BlockSpawnerController] Initialization complete");
        */
    }

    /// <inheritdoc/>
    public void Initialize(GameObject brick1x1, GameObject brick1x2, GameObject brick1x4, GameObject brick1x6)
    {
        // Validate inputs
        Assert.IsNotNull(model, "[BlockSpawnerController] Model is null in Initialize!");
        Assert.IsNotNull(brick1x1, "[BlockSpawnerController] brick1x1 cannot be null");
        Assert.IsNotNull(brick1x2, "[BlockSpawnerController] brick1x2 cannot be null");
        Assert.IsNotNull(brick1x4, "[BlockSpawnerController] brick1x4 cannot be null");
        Assert.IsNotNull(brick1x6, "[BlockSpawnerController] brick1x6 cannot be null");

        Debug.Log("[BlockSpawnerController] Initialize called with 4 brick prefabs");


        //CONTROLLER SHOULD NOT HAVE ACCESS TO ALL OF THIS IT SHOULD BE IN THE MODEL
        /*
        // Set up the model with the brick prefabs 
        GameObject[] brickPrefabs = new GameObject[4];
        brickPrefabs[0] = brick1x1;
        brickPrefabs[1] = brick1x2;
        brickPrefabs[2] = brick1x4;
        brickPrefabs[3] = brick1x6;

        model.BrickPrefabs = brickPrefabs;
        model.CurrentBrickIndex = 0;

        // Debug logging prfabs
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            if (brickPrefabs[i] == null)
            {
                Debug.LogError("Brick prefab at index " + i + " is NULL!");
            }
            else
            {
                Debug.Log("Brick prefab at index " + i + ": " + brickPrefabs[i].name);
            }
        }
        */
    }
    
/*
    /// <inheritdoc/>
    public void SpawnNextBrick()
    {
        Assert.IsNotNull(model, "Model is null in SpawnNextBrick");
        Assert.IsNotNull(view, "View is null in SpawnNextBrick");

        // Destroy the previous brick if it exists
        
        GameObject previousBrick = model.LastSpawnedBrick;
        if (previousBrick != null)
        {
            Debug.Log("[BlockSpawnerController] Destroying previous brick: " + previousBrick.name);
            view.DestroyBrick(previousBrick);
            model.LastSpawnedBrick = null;
        }

        // Get current brick prefab from model
        GameObject[] brickPrefabs = model.BrickPrefabs;
        int currentIndex = model.CurrentBrickIndex;

        Assert.IsNotNull(brickPrefabs, "BrickPrefabs array is null in SpawnNextBrick!");
        Assert.IsTrue(currentIndex >= 0 && currentIndex < brickPrefabs.Length, "CurrentBrickIndex " + currentIndex + " is out of bounds");

        GameObject prefabToSpawn = brickPrefabs[currentIndex];

        if (prefabToSpawn == null)
        {
            Debug.LogError("Brick prefab at index " + currentIndex + " is null!");
            return;
        }

        // Calculate spawn position
        Transform spawnTransform = model.SpawnArea;
        float height = model.SpawnHeight;
        Vector3 spawnPosition = spawnTransform.position + Vector3.up * height;

        Debug.Log("[BlockSpawnerController] Spawning brick at position: " + spawnPosition);

        // Instantiate the brick through the view
        GameObject spawnedBrick = view.InstantiateBrick(prefabToSpawn, spawnPosition, Quaternion.identity, model.BrickScale);

        // Configure brick visuals and physics
        view.ConfigureBrickVisuals(spawnedBrick);

        // Store reference to the spawned brick
        model.LastSpawnedBrick = spawnedBrick;

        // Cycle to next brick type
        int nextIndex = (currentIndex + 1) % brickPrefabs.Length;
        model.CurrentBrickIndex = nextIndex;
        
    }
    */

    public void SpawnBlock()
    {
        // to be implemented
    }

}
