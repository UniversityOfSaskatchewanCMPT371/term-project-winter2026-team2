using UnityEngine;

/// <summary>
/// View component of DeleteBlockView.
/// Handle the OnColliderEnter event and pass it to the controller.
/// </summary>
public class DeleteBlockView : 
    View<IDeleteBlockController>, // TODO reminder to switch the generic to the one you've implemented
    IDeleteBlockView
{

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }

    /// <inheritdoc/>
    public void OnColliderEnter(Collider collider)
    {
        Assert.IsNotNull(collider, "collider parameter cannot be null");
        controllerInstance.HandleColliderEnter(collider);
    }


}

