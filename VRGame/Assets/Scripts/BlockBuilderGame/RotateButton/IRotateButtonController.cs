/// <summary>
/// Interface for the RotateButton Controller.
/// Manages the logic for rotating the last spawned block by 90 degrees.
/// </summary>
public interface IRotateButtonController : IController
{
    /// <summary>
    /// Initializes the controller component
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures Init() is called
    new void Awake();

    /// <summary>
    /// Initializes the controller and checks model and view references.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (modelInstance != null) && (viewInstance != null) && (blockSpawnerModel != null)
    /// post-condition:
    ///     - ensures controller is ready to handle button presses
    /// </remarks>
    new void Init();

    /// <summary>
    /// Rotates the spawned block by 90 degrees on the y-axis.
    /// Does nothing if no block has been spawned yet.
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires none
    /// post-conditions:
    ///     - ensures block is rotated 90 degrees on the y-axis if LastSpawnedBlock != null.
    ///         Otherwise, logs a warning and returns early
    /// </remarks>
    void OnButtonPressed();
}
