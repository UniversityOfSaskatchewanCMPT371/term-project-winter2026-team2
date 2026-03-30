using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// View component of DeleteBlockView.
/// </summary>
public class DeleteBlockView : 
    View<IDeleteBlockController>, // TODO reminder to switch the generic to the one you've implemented
    IDeleteBlockView
{
    // use 'this.controllerInstance' to access controller component

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }
}

