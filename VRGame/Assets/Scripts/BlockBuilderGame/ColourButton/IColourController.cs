/// <summary>
/// Interface for the ColourButton Controller.
/// Manages the logic for cycling the spawned block's material on button press.
/// </summary>
public interface IColourController : IController
{
    /// <summary>
    /// Checks references for model and view component
    /// </summary>
    new void Awake();

    /// <summary>
    /// Initializes the controller and checks model, view, and block spawner references
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (modelInstance != null) && (viewInstance != null) && (blockSpawnerModel != null)
    /// post-condition:
    ///     - ensures controller is ready to handle button presses
    /// </remarks>
    new void Init();

    /// <summary>
    /// Sets colour material based on (spawned) block's index.
    /// Does nothing if no block has been spawned yet.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (modelInstance.Colours.Length > 0)
    /// post-conditions:
    ///     - ensures colour material is set on the spawned block prefab based on index if LastSpawnedBlock != null.
    ///         Otherwise, logs a warning and returns early
    /// </remarks>
    void OnButtonPressed();
}
