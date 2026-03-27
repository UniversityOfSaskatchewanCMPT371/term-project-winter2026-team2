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
        if (blockSpawnerModel != null)
        {
            blockSpawnerModel = blockSpawnerModel as IBlockSpawnerModel;
            if (blockSpawnerModel == null)
            {
                Debug.LogWarning("'blockSpawnerModel' does not implement IBlockSpawnerModel.");
            }
        }

        Assert.IsNotNull(blockSpawnerModel, "'blockSpawnerModel' inspector field must be assigned to the Block Spawner GameObject.");
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        // Get access to the block spawned
        GameObject block = blockSpawnerModel.LastSpawnedBlock;
        Assert.IsNotNull(block, "LastSpawnedBlock must not be null to change colour");

        // Get colours assigned in Inspector
        Material[] colours = modelInstance.Colours;
        Assert.IsTrue(colours.Length > 0, "Colours[] must not be empty");

        // Get access to the renderer component (materials live on Renderer component)
        int index = modelInstance.CurrentIndex;
        Renderer renderer = block.GetComponentInChildren<Renderer>();
        Assert.IsNotNull(renderer, "No Renderer found on LastSpawnedBlock");

        // Set colour material based on index
        renderer.material = colours[index];
        modelInstance.CurrentIndex = (index + 1) % colours.Length;
    }
}
