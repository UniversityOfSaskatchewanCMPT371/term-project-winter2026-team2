using UnityEngine;

/// <summary>
/// Base class for view component.
/// </summary>
/// <remarks>
/// - C type is the interface of the controller component you implemented.
/// This generic is used to declare type and typecast.
/// </remarks>
public abstract class View<C> : MonoBehaviour, IView
    where C : IController
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for controller
    /// </summary>
    [SerializeField]
    protected Controller<IModel, IView> serializableController;

    /// <summary>
    /// Controller portion of door module. Controller portion uses data from this
    /// </summary>
    protected C controller;

    /// <summary>
    /// Public accessor of controller portion of door module
    /// </summary>
    internal C Controller
    {
        /// <summary>
        /// Set the value of View's controller instance variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditions:
        /// - View's `controller` instance variable set to input value
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to set controller is null");
                Debug.Assert(value != null, "controller must not be null");
            }
            controller = value;
        }
    }

    /// <summary>
    /// Called by Start() method at runtime.
    /// Verifies the references to other layer components.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'controller' field is not missing.
    /// Postcondition:
    /// - Logs error if any field is missing.
    /// </remarks>
    private void CheckLayerRefs()
    {
        if (serializableController != null)
        {
            controller = (C)(IController)serializableController;
        }

        if (controller == null)
        {
            Debug.LogError("'controller' field is null.");
            Debug.Assert(controller != null, "'controller' field cannot be null.");
        }
    }

    /// </inheritdoc>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls.
    /// Inherited from MonoBehaviourl.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - Init() method is implemented.
    /// Postcondition:
    /// - Calls Init() method.
    /// <remarks>
    public virtual void Start()
    {
        CheckLayerRefs();
        Init();
    }
}