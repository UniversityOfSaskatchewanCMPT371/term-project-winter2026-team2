using UnityEngine;

/// <summary>
/// Base class for controller component.
/// </summary>
public abstract class Controller : MonoBehaviour, IController
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for model
    /// </summary>
    private Model serializableModel;

    /// <summary>
    /// Model portion of door module. Controller portion uses data from this
    /// </summary>
    private IModel model;

    /// <summary>
    /// Public accessor of model portion of door module
    /// </summary>
    internal IModel Model
    {

        /// <summary>
        /// Set the value of Controller's model instance variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditions:
        /// - Controller's `model` instance variable set to input value
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to set model is null");
                Debug.Assert(value != null, "model must not be null");
            }
            model = value;
        }
    }

    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for view
    /// </summary>
    private View serializableView;

    /// <summary>
    /// View portion of door module. Controller portion uses data from this
    /// </summary>
    private IView view;

    /// <summary>
    /// Public accessor of view portion of door module
    /// </summary>
    internal IView View
    {

        /// <summary>
        /// Set the value of Controller's view instance variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditions:
        /// - Controller's `view` instance variable set to input value
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to set View is null");
                Debug.Assert(value != null, "view must not be null");
            }
            view = value;
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
        Init();
    }
}