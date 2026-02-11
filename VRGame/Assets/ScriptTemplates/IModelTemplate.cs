/// <summary>
/// Template interface for Model layer.
/// </summary>
public interface IModelTemplate
{
    /*
    =================== PUBLIC METHODS SECTION ===================
    */

    /// <summary>
    /// Get the current value of Example.
    /// </summary>
    /// <returns>The value Example.</returns>
    int GetExample();

    /// <summary>
    /// Set the current value of Example
    /// </summary>
    /// <param name="amount">Value to be set to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Example cannot be negative.</exception>
    void SetExample(int amount);

    /// <summary>
    /// Increments the Example by 1.
    /// </summary>
    /// <exception cref="InvalidOperationException">Example cannot be incremented further.</exception>
    void IncrementExample();
}