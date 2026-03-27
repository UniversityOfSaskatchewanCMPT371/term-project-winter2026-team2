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

    
}
