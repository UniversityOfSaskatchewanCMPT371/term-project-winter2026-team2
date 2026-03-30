using UnityEngine;
/// <summary>
/// Interface for the CheckArea View
/// Handles trigger events for the check area and passes them to the controller
/// </summary>
public interface ICheckAreaView : IView
{
    /// <summary>
    /// Initializes insideColliders reference
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-condition:
    ///     - ensures controllerInstance is checked
    /// </remarks>
    new void Init();

    /// <summary>
    /// Sends enter trigger events to controller
    /// <param name="collider">The collider that entered the trigger</param>
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires onEnter != null
    /// post-condition:
    ///     - ensures onEnter events are sent to controller
    /// </remarks>
    void OnEnter(Collider collider);

    /// <summary>
    /// Sends exit trigger events to controller
    /// <param name="collider">The collider that exited the trigger</param>
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires onExit != null
    /// post-condition:
    ///     - ensures onExit events are sent to controller
    /// </remarks>
    void OnExit(Collider collider);

    
}
