using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller interface for component of PlayerService.
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
    /// - 'XRrigPrefab' variable cannot be null.
    /// - 'XRrigPrefab' has PlayerController component.
    /// - The TeleportPlayerTo() method must be implemented in PlayerController.
    /// Postconditions:
    /// - 'playerObj' variable value is set to the new instantiated XR rig, 
    /// and teleported/orientated to the given 'position' and 'rotation' input.
    /// - if 'player' field is already set, then the existing rig is teleported/orientated instead.
    /// </remarks>
    void SpawnPlayer(Vector3 position, Quaternion rotation);

    /// <summary>
    /// Initializes the singleton instance and validates the required prefab references.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'XRrigPrefab' must be set in the inspector.
    /// Postconditions:
    /// - 'singleton' field is assigned to this instance.
    /// - Any existing duplicate 'PlayerServiceController' instances are destroyed.
    /// </remarks>
    void Init();
}
