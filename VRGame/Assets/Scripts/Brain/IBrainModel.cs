/// <summary>
/// Interface for Brain model component
/// </summary>
public interface IBrainModel : IModel
{
    /// <summary>
    /// Guarantees model is fully initialized before Start() runs
    /// </summary>
    void Awake();

    /// <summary>
    /// Overrides base Start() to prevent double-initialization since Awake() already called Init()
    /// </summary>
    void Start();

    /// <summary>
    /// Method to initialize the model component
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires animator == null
    /// Post-condition:
    ///     ensures (animator != null) &&
    ///     (animator is assigned to the Animator component)
    /// </remarks>
    new void Init();

    /// <summary>
    /// Pauses the animation by setting the animation speed to 0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires animator != null
    /// Post-condition:
    ///     modifies animator.speed == 0f
    /// </remarks>
    void pause();

    /// <summary>
    /// Resumes the animation by setting the animation speed to 1.0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires animator != null
    /// Post-condition:
    ///     modifies animator.speed == 1.0f
    /// </remarks>
    void resume();

}
