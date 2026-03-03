/// <summary>
/// Interface for view component.
/// </summary>
public interface IView
{   
    /// <summary>
    /// Resolves and validates the controller
    /// reference required by this component.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'inspectorWindowController' may or may not
    ///   be assigned in the inspector. If unassigned, matching component
    ///   implementing <typeparamref name="C"/> may
    ///   be auto‑assigned when the inspector field is unset.
    /// Postcondition:
    /// - 'controllerInstance' is resolved from a component implementing
    ///   <typeparamref name="C"/>.
    /// - Errors are logged and assertions raised when either reference is missing
    ///   or does not implement the required interface.
    /// </remarks>
    void CheckControllerRef();

    /// <summary>
    /// Called by Start() method at runtime.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}