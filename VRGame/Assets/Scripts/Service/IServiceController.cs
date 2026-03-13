
public interface IServiceController : IController
{
    /// <summary>
    /// Initializes the component and assures that it follows
    /// a persistent singleton pattern.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Destroy duplicate instances of this instance if any exists. Otherwise, sets the
    /// variable 'instance' to 'this' class, and keeps the gameobject persistent.
    /// - Logs if initialization is successful or if this instance is a duplicate.
    /// </remarks>
    new void Init();
}