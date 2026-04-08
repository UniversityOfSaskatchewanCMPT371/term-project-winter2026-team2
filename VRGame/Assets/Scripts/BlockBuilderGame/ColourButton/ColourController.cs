using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller component of ColourController.
/// </summary>
public class ColourController : Controller<IColourModel, IColourView>, IColourController
{
    /// <summary>
    /// Serialized reference to the BlockSpawner model set via the inspector
    /// </summary>
    [SerializeField] private ModelComponent BlockSpawnerModel;

    /// <summary>
    /// Reference to the BlockSpawner model to read LastSpawnedBlock from
    /// </summary>
    private IBlockSpawnerModel blockSpawnerModel;

    /// <inheritdoc/>
    public void Awake()
    {
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// <inheritdoc/>
    public override void Start()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Init()
    {
        if (BlockSpawnerModel == null)
        {
            Debug.LogError("'BlockSpawnerModel' inspector field must be assigned to the Block Spawner GameObject");
            Assert.IsNotNull(blockSpawnerModel, "'BlockSpawnerModel' must not be null");
            return;
        }

        blockSpawnerModel = BlockSpawnerModel as IBlockSpawnerModel;

        // Check again since we assigned in previous line
        if (blockSpawnerModel == null)
        {
            Debug.LogError("'BlockSpawnerModel' must implement IBlockSpawnerModel");
            Assert.IsNotNull(blockSpawnerModel, "'BlockSpawnerModel' must implement IBlockSpawnerModel");
            return;
        }
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        // Get access to the block spawned
        GameObject block = blockSpawnerModel.LastSpawnedBlock;
        if (block == null)
        {
            Debug.LogError("No spawned block to change colour. Please spawn a block first by pressing the 'Spawn' button.");
            return;
        }

        // Get colours assigned in Inspector
        Material[] colours = modelInstance.Colours;
        if (colours == null)
        {
            Debug.LogError("Colours[] must not be null");
            Assert.IsNotNull(colours, "Colours[] must not be null");
            return;
        }
        if (colours.Length == 0)
        {
            Debug.LogError("Colours[] must not be empty");
            Assert.IsTrue(colours.Length > 0, "Colours[] must not be empty");
            return;
        }

        // Get access to the renderer component (materials live on Renderer component)
        int index = modelInstance.CurrentIndex;
        if (index < 0 || index >= colours.Length)
        {
            Debug.LogError("Index out of bounds for Colours[]");
            Assert.IsTrue(index >= 0 && index < colours.Length, "Index out of bounds for Colours[]");
            return;
        }
    

        Renderer renderer = block.GetComponentInChildren<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("No Renderer found on LastSpawnedBlock");
            Assert.IsNotNull(renderer, "No Renderer found on LastSpawnedBlock");
            return;
        }
        
        // Set colour material based on index
        renderer.material = colours[index];
        modelInstance.CurrentIndex = (index + 1) % colours.Length;
    }
}
