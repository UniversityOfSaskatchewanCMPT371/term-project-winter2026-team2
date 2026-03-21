/// <summary>
/// Controller Portion of the reusable door module. Interaction logic is handled here
/// </summary>
/// <remarks>
/// - doorModel, sceneChangerController must be set before calling Init();
/// </remarks>
public interface IDoorController
{

    /// <summary>
    /// Public readonly accessor for triggerDebounce
    /// </summary>
    public bool TriggerDebounce
    {
        /// <summary>
        /// View current status of triggerDebounce
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - triggerDebounce is returned 
        get;
    }

    /// <summary>
    /// Handles logic for player entering a door
    /// </summary>
    /// <param name="playerController"> Controller portion of player module </param>
    /// <remarks>
    /// Preconditions:
    /// - playerController must be non-null
    /// PostConditions:
    /// - Changes to state made in argument, `teleportPlayerTo` called on playerController,
    /// moving them to position of `doorModel`s target door
    /// </remarks>
    void OnPlayerEnter(IPlayerController player);

    /// <summary>
    /// Initializes the DoorController. Called by the game within the
    /// MonoBehaviour function `Start()` (executes the frame a script is enabled)
    /// - Separated from `Start()`, as this makes unit testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `doorModel` must be non-null
    /// - Service.prefab exists within the scene
    /// PostConditions:
    /// - `scenecChangerController` instance variable is automatically set to SceneChangerController component within Service.prefab
    /// - Checked to make sure DoorController will be able to function properly, has values set for instance variables
    /// </remarks>
    public void Init();
}