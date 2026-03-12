using UnityEngine;

public class ServiceController : Controller<IModel, IView>, IServiceController
{
    internal ServiceController instance;

    [SerializeField]
    public static PlayerServiceController PlayerService;

    /// <summary>
    /// Initializes the component and assures that it follows
    /// a persistent singleton pattern.
    /// </summary>
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
    private void Awake() {
        Init();
    }

    /// <summary>
    /// Called once after all Awake() calls.
    /// This method does nothing, but overrides the default Start() defined in
    /// Controller base class.
    /// </summary
    public override void Start() {}
}