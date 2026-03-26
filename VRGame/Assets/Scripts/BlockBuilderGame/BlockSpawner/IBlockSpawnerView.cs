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

    /// <summary>
    /// Sets up XR interactable listeners on child components
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     -   requires (controllerInstance != null) and
    ///                 at least one XRBaseInteractable child must exist
    /// post-conditions:
    ///     -   ensures selectEntered listeners are registered on all child XRBaseInteractables
    /// </remarks>
    void SetupXREvents();
}
