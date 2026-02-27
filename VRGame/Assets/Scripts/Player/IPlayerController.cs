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

    /*
    IMPORTANT NOTE: I'm not working on the player component, but the controller
    portion of the doors interacts with it so I need an interface. This is 
    a placeholder interface that will likely change
    */
    /// <summary>
    /// Teleports the player to a specified destination oriented in a specified rotation.
    /// </summary>
    /// <param name="position">The position in which the player rig is teleported to.</param>
    /// <param name="rotation">The position in which the player rig is oriented to.</param>
    void teleportPlayerTo(Vector3 position, Quaternion rotation);
}
