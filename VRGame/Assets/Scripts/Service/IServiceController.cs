
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

    /// <summary>
    /// Called once after the scene loads. 
    /// Initializes this component by calling Init().
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Init() is implemented.
    /// Postconditions:
    /// - Init() is invoked.
    /// </remarks>
    void Awake();

    /// <summary>
    /// Called once after all Awake() calls.
    /// This method does nothing, but overrides the default Start() defined in
    /// Controller base class.
    /// </summary
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - None
    /// </remarks>
    void Start();
}