/// <summary>
/// Interface for Brain model component
/// </summary>
public interface IBrainModel : IModel
{
    /// <summary>
    /// Method to initialize the model component
    /// </summary>
    new void Init();

    /// <summary>
    /// Pauses the animation by setting the animation speed to 0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires (model != null) && (animator != null)
    /// Post-condition:
    ///     ensures animator.speed == 0
    /// </remarks>
    void pause();

    /// <summary>
    /// Resumes the animation by setting the animation speed to 1.0f
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires (model != null) && (animator != null)
    /// Post-condition:
    ///     ensures animator.speed == 1.0f
    /// </remarks>
    void resume();

}
