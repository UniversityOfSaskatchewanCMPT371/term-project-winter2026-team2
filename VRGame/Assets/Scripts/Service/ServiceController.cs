using UnityEngine;

public class ServiceController : Controller<IModel, IView>, IServiceController
{
    /// <summary>
    /// Reference to the singleton instance of this class. Used to
    /// follow singleton pattern.
    /// </summary>
    private static ServiceController instance;

    /// <inheritdoc/>
    public override void Init()
    {
        // see if an instance of this component already exists
        if (instance != null && instance != this)
        {   
            // destroy the instance to follow singleton pattern
            Destroy(gameObject);

            Debug.Log("An instance of this singleton already exists.");
            return;
        } else
        {
            // declare this component the instance
            instance = this;

            // keep this game object persistent
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("ServiceController initialized successfully.");
    }

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
    public void Awake() 
    {
        Init();
    }

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
    public override void Start() {}
}