/// <summary>
/// Interface for the BlockSpawner View.
/// Handles wiring XR button events to the controller.
/// </summary>
public interface IBlockSpawnerView : IView
{
    /// <summary>
    /// Checks for Controller referevnce and 
    /// Calls on SetUpXREvents to initialize XR events
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     -   requires controllerInstance != null
    /// post-conditions:
    ///     -   ensures XR events initialized
    new void Init();
}
