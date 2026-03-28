using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// View component of SnapFitView.
/// </summary>
public class SnapFitView : 
    View<IController>, // TODO reminder to switch the generic to the one you've implemented
    ISnapFitView
{
    // use 'this.controllerInstance' to access controller component

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }
}

