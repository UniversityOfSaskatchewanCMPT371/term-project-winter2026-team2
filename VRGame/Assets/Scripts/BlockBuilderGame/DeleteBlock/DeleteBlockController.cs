using UnityEngine;

/// <summary>
/// Controller component of DeleteBlockController.
/// </summary>
public class DeleteBlockController : 
    Controller<IDeleteBlockModel, IDeleteBlockView>, // TODO reminder to switch the generics to the ones you've implemented
    IDeleteBlockController
{

    /// <inheritdoc/>
    public override void Init()
    {
        // these are used to resolve and validate model and view components
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// <inheritdoc/>
    public void HandleColliderEnter(Collider collider)
    {
        Assert.IsNotNull(collider, "collider parameter cannot be null");
        Destroy(collider.gameObject);
    }
}
