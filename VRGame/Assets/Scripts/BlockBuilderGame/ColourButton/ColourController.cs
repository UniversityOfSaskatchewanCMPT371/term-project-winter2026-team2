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
        if (BlockSpawnerModel != null)
        {
            blockSpawnerModel = BlockSpawnerModel as IBlockSpawnerModel;
            if (blockSpawnerModel == null)
            {
                Debug.LogWarning("'BlockSpawnerModel' does not implement IBlockSpawnerModel.");
            }
        }

        Assert.IsNotNull(blockSpawnerModel, "'BlockSpawnerModel' inspector field must be assigned to the Block Spawner GameObject.");
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        // Get access to the block spawned
        GameObject block = blockSpawnerModel.LastSpawnedBlock;
        if (block == null)
        {
            Debug.LogWarning("No spawned block to change colour. Please spawn a block first by pressing the 'Spawn' button.");
            return;
        }

        // Get colours assigned in Inspector
        Material[] colours = modelInstance.Colours;
        Assert.IsTrue(colours.Length > 0, "Colours[] must not be empty");
        Assert.IsNotNull(colours, "Colours[] must not be null");

        // Get access to the renderer component (materials live on Renderer component)
        int index = modelInstance.CurrentIndex;
        Renderer renderer = block.GetComponentInChildren<Renderer>();
        Assert.IsNotNull(renderer, "No Renderer found on LastSpawnedBlock");
        Assert.IsTrue(index >= 0 && index < colours.Length, "Index out of bounds for Colours[]");

        // Set colour material based on index
        renderer.material = colours[index];
        modelInstance.CurrentIndex = (index + 1) % colours.Length;
    }
}
