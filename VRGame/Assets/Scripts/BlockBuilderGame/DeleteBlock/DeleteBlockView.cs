using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View component of DeleteBlockView.
/// Handle the OnColliderEnter event and pass it to the controller.
/// </summary>
public class DeleteBlockView : 
    View<IDeleteBlockController>,
    IDeleteBlockView
{

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }

    /// <inheritdoc/>
    public void OnTriggerEnter(Collider collider)
    {
        Assert.IsNotNull(collider, "collider parameter cannot be null");

        // Only restrict to spawned block prefabs
        if (collider.GetComponent<SnapFitController>() == null)
        {
            return;
        }

        controllerInstance.HandleColliderEnter(collider);
    }
}

