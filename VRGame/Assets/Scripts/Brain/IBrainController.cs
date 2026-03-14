// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this
// If you are making Model layer, inherit from IModel.
// Same goes for other layers. (IController/IView)

/// <summary>
/// TODO: Change the docstring to match your implementation.
/// </summary>
public interface IBrainController : IController
{
    /// <summary>
    /// 
    /// </summary>
    new void Init();

    /// <summary>
    /// Pauses the animation
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires modelInstance != null
    /// Post-condition:
    ///     ensures modelInstance.pause() is invoked
    /// </remarks>
    void OnHoverEnter();

    /// <summary>
    /// Resumes the animation
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires modelInstance != null
    /// Post-condition:
    ///     ensures modelInstance.resume() is invoked
    /// </remarks>
    void OnHoverExit();
}
