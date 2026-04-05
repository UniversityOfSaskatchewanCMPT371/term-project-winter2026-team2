using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller component of SpawnButtonController.
/// Manages the logic for triggering block spawning via button press.
/// </summary>
public class SpawnButtonController : Controller<ISpawnButtonModel, ISpawnButtonView>, ISpawnButtonController
{
    /// <summary>
    /// Reference to the BlockSpawner controller (set via inspector window)
    /// </summary>
    [SerializeField] private ControllerComponent blockSpawnerController;

    /// <summary>
    /// Reference to the BlockSpawner controller to invoke SpawnBlock() on button press
    /// </summary>
    private IBlockSpawnerController blockSpawner;

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

        // Implement from IBlockSpawnerController
        if (blockSpawnerController is IBlockSpawnerController spawner)
        {
            blockSpawner = spawner;
            Assert.IsNotNull(spawner, "spawner must not be null on Init");
        }
        else if (blockSpawnerController != null)
        {
            Debug.LogWarning("BlockSpawner controller does not implement from IBlockSpawnerController");
        }

        // Access BlockSpawnerController via its interface
        if (blockSpawner == null)
        {
            blockSpawner = gameObject.GetComponent<IBlockSpawnerController>();
            Assert.IsNotNull(blockSpawner, "blockSpawner must not be null on Init");
        }
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        if (blockSpawner == null)
        {
            Debug.LogError("blockSpawner cannot be null OnButtonPressed");
        }
        Assert.IsNotNull(blockSpawner, "blockSpawner must not be null on ButtonPressed");
        blockSpawner.SpawnBlock();
    }
}
