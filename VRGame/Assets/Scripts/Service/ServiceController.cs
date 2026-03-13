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

            Debug.LogWarning("There can be only one active ServiceController.");
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

    /// <inheritdoc/>
    public void Awake() 
    {
        Init();
    }

    /// <inheritdoc cref="IServiceController" />
    public override void Start() {}
}