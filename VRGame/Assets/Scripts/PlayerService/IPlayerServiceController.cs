using UnityEngine;

/// <summary>
/// Controller interface for PlayerServiceController component.
/// </summary>
public interface IPlayerServiceController : IController
{
    /// <summary>
    /// Instantiates the player rig at specified position and orientation.
    /// </summary>
    /// <param name="position">The vector in which to transform the rig's position to.</param>
    /// <param name="rotation">The quaternion in which to orientate the rig's rotation to.</param>
    /// <remarks>
    /// Preconditions:
    /// - 'XRrigPrefab' must be valid.
    /// - requires TeleportPlayerTo() method defined at PlayerController.
    /// Postconditions:
    /// - 'playerObj' variable value is set to the new instantiated XR rig, 
    /// and teleported/orientated to the given 'position' and 'rotation' input.
    /// - if 'player' field is already set, then the existing rig is teleported/orientated instead.
    /// </remarks>
    void SpawnPlayer(Vector3 position, Quaternion rotation);

    /// <summary>
    /// Initializes and validates the component and enforces singleton pattern. Also
    /// spawns the player on scene load if enabled.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'XRrigPrefab' variable must be non-null, and contain PlayerController component.
    /// Postconditions:
    /// - 'instance' variable is assigned to 'this' component if not already assigned.
    /// Otherwise remains unchanged and destroys the game object its attached to.
    /// - Optionally spawns the player by invoking SpawnPlayer() 
    /// if 'spawnPlayerOnLoad' variable is true.
    /// - Logs warnings and errors if preconditions are violated.
    /// - Logs on success.
    /// </remarks>
    new void Init();

    /// <summary>
    /// Called once after the scene loads. 
    /// Initializes this component by calling Init().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Init() is implemented.
    /// Postconditions:
    /// - Init() is invoked.
    /// </remarks>
    void Awake();

    /// <summary>
    /// Called once after all Awake() calls.
    /// This method does nothing, but overrides the default Start() defined in
    /// Controller base class.
    /// </summary
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - None
    /// </remarks>
    void Start();
}
