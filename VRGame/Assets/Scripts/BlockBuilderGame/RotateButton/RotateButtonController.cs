using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class for RotateButton.
/// Rotates the last spawned block by 90 degrees on the Y axis each press.
/// </summary>
public class RotateButtonController : Controller<IRotateButtonModel, IRotateButtonView>, IRotateButtonController
{
    /// <summary>
    /// Serialized reference to the BlockSpawner model set via the inspector.
    /// </summary>
    [SerializeField] private ModelComponent BlockSpawnerModel;

    /// <summary>
    /// Reference to the BlockSpawner model to read LastSpawnedBlock from
    /// </summary>
    private IBlockSpawnerModel blockSpawnerModel;

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

        if (BlockSpawnerModel != null)
        {
            blockSpawnerModel = BlockSpawnerModel as IBlockSpawnerModel;
            if (blockSpawnerModel == null)
            {
                Debug.LogWarning("'BlockSpawnerModel' does not implement IBlockSpawnerModel.");
            }
        }

        if (blockSpawnerModel == null)
        {
            blockSpawnerModel = gameObject.GetComponent<IBlockSpawnerModel>();
        }

        Assert.IsNotNull(blockSpawnerModel, "'blockSpawnerModel' field cannot be null.");
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        GameObject block = blockSpawnerModel.LastSpawnedBlock;
        Assert.IsNotNull(block, "blockSpawnerModel must not be null to rotate");

        block.transform.Rotate(0f, 90f, 0f);
    }
}
