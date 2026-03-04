/// <summary>
/// Interface for controller component.
/// </summary>
public interface IController
{
    /// <summary>
    /// Validates and initializes the the 'modelInstance' field.
    /// Tries to deserialize 'inspectorWindowModel' when not null.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'inspectorWindowModel' must be non-null OR a matching component
    ///   implementing <typeparamref name="M"/> must be non-null.
    /// Postconditions:
    /// - IFF 'inspectorWindowModel' was non-null, 'modelInstance' will be set
    ///   to a deserialized copy
    /// - IFF 'inspectorWindowModel' was null AND the above described matching
    ///   component was present and not null, 'modelInstance' will be set to a
    ///   typecast copy
    /// </remarks>
    void CheckModelRef();

    /// <summary>
    /// Validates and initializes the the 'viewInstance' field.
    /// Tries to deserialize 'inspectorWindowView' when not null.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'inspectorWindowView' must be non-null OR a matching component
    ///   implementing <typeparamref name="V"/> must be non-null.
    /// Postconditions:
    /// - IFF 'inspectorWindowView' was non-null, 'viewInstance' will be set
    ///   to a deserialized copy
    /// - IFF 'inspectorWindowView' was null AND the above described matching
    ///   component was present and not null, 'viewInstance' will be set to a
    ///   typecast copy
    /// </remarks>
    void CheckViewRef();

    /// <summary>
    /// Called by Start() method at runtime.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}