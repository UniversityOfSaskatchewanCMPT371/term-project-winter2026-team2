/// <summary>
/// Interface for model component.
/// </summary>
public interface IModel
{
    /// <summary>
    /// Called by Start() method at runtime.
    /// Initializes the component and verifies each field values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - All fields set and values are valid.
    /// Postconditions:
    /// - Component is initialized and no errors occur.
    /// </remarks>
    void Init();
}