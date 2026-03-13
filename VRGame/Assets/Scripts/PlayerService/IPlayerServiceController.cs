using UnityEngine;

/// <summary>
/// Controller interface for PlayerServiceController component.
/// </summary>
public interface IPlayerServiceController
{
    /// <summary>
    /// Instantiates the player rig at specified position and orientation.
    /// </summary>
    /// <param name="position">The vector in which to transform the rig's position to.</param>
    /// <param name="rotation">The quaternion in which to orientate the rig's rotation to.</param>
    /// <remarks>
    /// Preconditions:
    /// - 'XRrigPrefab' must be validated.
    /// - The TeleportPlayerTo() method must be implemented in PlayerController.
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
    /// - 'instance' variable must be null.
    /// - 'XRrigPrefab' variable must be set in the inspector.
    /// - 'XRrigPrefab' has PlayerController component.
    /// Postconditions:
    /// - 'instance' variable is assigned to this component. Any duplicate 
    /// instances of this component is destroyed.
    /// - Optionally spawns the player by invoking SpawnPlayer() 
    /// if 'spawnPlayerOnLoad' variable is true.
    /// - Logs warnings and errors if preconditions are violated.
    /// - Logs on success.
    /// </remarks>
    void Init();

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
