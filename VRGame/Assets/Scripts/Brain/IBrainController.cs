/// <summary>
/// Controller component of BrainController that manages hover interactions
/// </summary>
public interface IBrainController : IController
{
    /// <summary>
    /// Called before Start()
    /// Ensures controller is ready before view Sets up XR events
    /// </summary>
    void Awake();

    /// <summary>
    /// Initializes the model and view instance
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires (modelInstance == null) && (viewInstance == null)
    /// Post-condition:
    ///     ensures (modelInstance != null) && (viewInstance != null)
    /// </remarks>
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
