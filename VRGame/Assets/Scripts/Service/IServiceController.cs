
public interface IServiceController : IController
{
    /// <summary>
    /// Initializes the component and assures that it follows
    /// a persistent singleton pattern.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'instance' variable must be null.
    /// Postconditions:
    /// - Destroy duplicate instances of this instance if any exists. Otherwise, sets the
    /// variable 'instance' to 'this' class, and keeps the gameobject persistent.
    /// - Logs warning if this instance is a duplicate. Otherwise, logs a successful initialization.
    /// </remarks>
    new void Init();
}