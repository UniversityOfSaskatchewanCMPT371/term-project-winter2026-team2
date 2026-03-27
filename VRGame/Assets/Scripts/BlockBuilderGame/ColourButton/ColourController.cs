using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// Controller component of ColourController.
/// </summary>
public class ColourController : 
    Controller<IColourModel, IColourView>, // TODO reminder to switch the generics to the ones you've implemented
    IColourController
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
