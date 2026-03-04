/// <summary>
/// Interface for view component.
/// </summary>
public interface IView
{
    /// <summary>
    /// Validates and initializes the the 'controllerInstance' field.
    /// Tries to deserialize 'inspectorWindowController' when not null.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'inspectorWindowController' must be non-null OR a matching component
    ///   implementing <typeparamref name="C"/> must be non-null.
    /// Postconditions:
    /// - IFF 'inspectorWindowController' was non-null, 'controllerInstance' will be set
    ///   to a deserialized copy
    /// - IFF 'inspectorWindowController' was null AND the above described matching
    ///   component was present and not null, 'controllerInstance' will be set to a
    ///   typecast copy
    /// </remarks>
    void CheckControllerRef();

    /// <summary>
    /// Called by Start() method at runtime.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}