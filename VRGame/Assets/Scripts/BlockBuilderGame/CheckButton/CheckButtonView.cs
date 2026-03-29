using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// View component of CheckButtonView.
/// </summary>
public class CheckButtonView : 
    View<ICheckButtonController>, // TODO reminder to switch the generic to the one you've implemented
    ICheckButtonView
{
    // use 'this.controllerInstance' to access controller component

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }
}

