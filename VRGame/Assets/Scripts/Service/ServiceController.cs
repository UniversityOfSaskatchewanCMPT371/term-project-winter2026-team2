using UnityEngine;

public class ServiceController : Controller<IModel, IView>, IServiceController
{
    /// <summary>
    /// Reference to the singleton instance of this class. Used to
    /// see if an instance of this class already exists in Init() method.
    /// </summary>
    internal static ServiceController instance;

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
            // make this the instance
            instance = this;

            // keep it persistent
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("ServiceController initialized successfully.");
    }

    /// <inheritdoc/>
    public void Awake() 
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Start() {}
}