/// <summary>
/// Interface for Brain model component
/// </summary>
public interface IBrainModel : IModel
{
    /// <summary>
    /// Method to initialize the model component
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires animator != null
    /// Post-condition:
    ///     animator is assigned to the Animator component
    /// </remarks>
    new void Init();

    /// <summary>
    /// Pauses the animation by setting the animation speed to 0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires (model != null) && (animator != null)
    /// Post-condition:
    ///     modifies animator.speed == 0f
    /// </remarks>
    void pause();

    /// <summary>
    /// Resumes the animation by setting the animation speed to 1.0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires (model != null) && (animator != null)
    /// Post-condition:
    ///     modifies animator.speed == 1.0f
    /// </remarks>
    void resume();

}
