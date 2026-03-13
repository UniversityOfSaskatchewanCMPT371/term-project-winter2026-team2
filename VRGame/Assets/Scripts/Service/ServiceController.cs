using UnityEngine;

public class ServiceController : Controller<IModel, IView>, IServiceController
{
    /// <summary>
    /// Reference to the singleton instance of this class. Used to
    /// follow singleton pattern.
    /// </summary>
    internal static ServiceController instance;

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