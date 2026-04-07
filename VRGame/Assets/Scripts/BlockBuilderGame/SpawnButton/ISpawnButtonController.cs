/// <summary>
/// Interface for the SpawnButton Controller.
/// Manages block spawning via button press.
/// </summary>
public interface ISpawnButtonController : IController
{
    /// <summary>
    /// Initializes the controller and checks model and view references.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (modelInstance != null) && (viewInstance != null) && (blockSpawner != null)
    /// post-condition:
    ///     - ensures controller is ready to handle button presses
    /// </remarks>
    new void Init();

    /// <summary>
    /// Triggers SpawnBlock on the referenced BlockSpawner controller.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires blockSpawner != null
    /// post-conditions:
    ///     - ensures blockSpawner.SpawnBlock() is called
    /// </remarks>
    void OnButtonPressed();
}
