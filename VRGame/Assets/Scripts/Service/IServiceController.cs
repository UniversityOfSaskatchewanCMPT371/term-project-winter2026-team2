
public interface IServiceController : IController
{
    /// <summary>
    /// Initializes this component. Called by Awake() at runtime instead of Start() to
    /// assure that it follows singleton pattern.
    /// </summary>
    void Init();
}