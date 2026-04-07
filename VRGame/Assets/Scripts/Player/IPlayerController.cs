using UnityEngine;

public interface IPlayerController
{
    /// <summary>
    /// Initialize the player controller and validate model/view references
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - model and view instance variables must be assigned
    /// Postconditions:
    /// - model and view instance variables are assigned and valid, player is initialized with default values
    /// </remarks>
    void Awake();

    /// <summary>
    /// Teleports the player to a specified destination oriented in a specified rotation.
    /// </summary>
    /// <param name="position">The position in which the player rig is teleported to.</param>
    /// <param name="rotation">The position in which the player rig is oriented to.</param>
    /// <remarks>
    /// Preconditions:
    /// - model instance variable must be assigned
    /// - params must be valid (non-NaN, non-infinite)
    /// Postconditions:
    /// - player rig is teleported to the specified position and oriented to the specified rotation
    /// </remarks>
    void teleportPlayerTo(Vector3 position, Quaternion rotation);
}
