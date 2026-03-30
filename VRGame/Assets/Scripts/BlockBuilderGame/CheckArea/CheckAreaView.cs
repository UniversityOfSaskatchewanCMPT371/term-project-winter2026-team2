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

    /// <inheritdoc/>
    public void OnEnter(Collider collider)
    {
        controllerInstance.OnEnter(collider);
    }

    /// <inheritdoc/>
    public void OnExit(Collider collider)
    {
        controllerInstance.OnExit(collider);
    }
}
