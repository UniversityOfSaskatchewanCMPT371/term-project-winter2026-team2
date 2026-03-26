/// <summary>
/// View component of BlockSpawnerView.
/// </summary>
public class BlockSpawnerView : View<IBlockSpawnerController>, IBlockSpawnerView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();
    }

    // Spawner is not interactable through View, no methods needed

}
