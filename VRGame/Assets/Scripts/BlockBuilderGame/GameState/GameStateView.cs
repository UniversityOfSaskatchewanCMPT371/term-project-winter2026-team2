using UnityEngine;

// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this

/// <summary>
/// View component of GameStateView.
/// </summary>
public class GameStateView : 
    View<IGameStateController>, // TODO reminder to switch the generic to the one you've implemented
    IGameStateView
{
    // use 'this.controllerInstance' to access controller component

    /// <inheritdoc/>
    public override void Init()
    {
        // this is used to resolve and validate the controller component
        this.CheckControllerRef();
    }
}

