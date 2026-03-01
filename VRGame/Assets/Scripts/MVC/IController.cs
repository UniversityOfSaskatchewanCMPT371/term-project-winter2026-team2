/// <summary>
/// Interface for controller component.
/// </summary>
public interface IController
{   
    /// <summary>
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