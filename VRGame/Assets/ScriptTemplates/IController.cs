/// <summary>
/// Interface for controller component.
/// </summary>
public interface IController
{   
    /// <summary>
    /// Resolves and validates the 'modelInstance' field required for this component.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'inspectorWindowModel' may or may not
    ///   be assigned in the inspector. If unassigned, matching component
    ///   implementing <typeparamref name="M"/> may
    ///   be auto‑assigned when the inspector field is unset.
    /// Postconditions:
    /// - 'modelInstance' is resolved from a component implementing
    ///   <typeparamref name="M"/>.
    /// - Errors are logged and assertions raised when either reference is missing
    ///   or does not implement the required interface.
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