/// <summary>
/// Interface for view component.
/// </summary>
public interface IView
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
