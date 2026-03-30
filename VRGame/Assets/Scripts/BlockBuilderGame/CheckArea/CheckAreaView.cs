using UnityEngine;

/// <summary>
/// View component of CheckArea.
/// Handles trigger events for the check area and passes them to the controller
/// </summary>
public class CheckAreaView : View<ICheckAreaController>, ICheckAreaView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();
    }

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
    private void OnTriggerEnter(Collider collider)
    {
        controllerInstance.OnEnter(collider);
    }

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
    private void OnTriggerExit(Collider collider)
    {
        controllerInstance.OnExit(collider);
    }
}
