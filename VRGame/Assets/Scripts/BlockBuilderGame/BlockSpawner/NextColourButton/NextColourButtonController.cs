using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// Controller component of NextColourButtonController.
/// </summary>
public class NextColourButtonController : 
    Controller<IModel, IView>, // TODO reminder to switch the generics to the ones you've implemented
    INextColourButtonController
{
    // use 'this.viewInstance' to access view component, and
    // 'this.modelInstance' to access model component

    /// <inheritdoc/>
    public override void Init()
    {
        // these are used to resolve and validate model and view components
        this.CheckModelRef();
        this.CheckViewRef();
    }
}
