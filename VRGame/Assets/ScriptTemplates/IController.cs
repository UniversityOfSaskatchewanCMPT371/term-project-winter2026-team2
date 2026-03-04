/// <summary>
/// Interface for controller component.
/// </summary>
public interface IController
{
    /// <summary>
    /// Validates and initializes the the 'modelInstance' field.
    /// Tries to deserialize 'inspectorWindowView' when not null.
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
    /// Resolves and validates the 'viewInstance' field required for this component.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'inspectorWindowView' may or may not
    ///   be assigned in the inspector. If unassigned, matching component
    ///   implementing <typeparamref name="V"/> may
    ///   be auto‑assigned when the inspector field is unset.
    /// Postconditions:
    /// - 'viewInstance' is resolved from a component implementing
    ///   <typeparamref name="V"/>.
    /// - Errors are logged and assertions raised when either reference is missing
    ///   or does not implement the required interface.
    /// </remarks>
    void CheckViewRef();

    /// <summary>
    /// Called by Start() method at runtime.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}