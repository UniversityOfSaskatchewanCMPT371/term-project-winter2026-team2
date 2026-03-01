using UnityEngine;

/// <summary>
/// Base class for controller component.
/// </summary>
/// <remarks>
/// - M type is the interface of the model component you implemented.
/// - V type is the interface of the view component you implemented.
/// These generics are used to typecast.
/// </remarks>
public abstract class Controller<M,V> : MonoBehaviour, IController
    where M : IModel
    where V : IView
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for model
    /// </summary>
    [SerializeField]
    protected Model serializableModel;

    /// <summary>
    /// Model portion of door module. Controller portion uses data from this
    /// </summary>
    protected M model;

    /// <summary>
    /// Public accessor of model portion of door module
    /// </summary>
    internal M Model
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
    [SerializeField]
    protected View<IController> serializableView;

    /// <summary>
    /// View portion of door module. Controller portion uses data from this
    /// </summary>
    protected V view;

    /// <summary>
    /// Public accessor of view portion of door module
    /// </summary>
    internal V View
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

    /// <summary>
    /// Called by Start() method at runtime.
    /// Verifies the references to other layer components.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'view' and 'model' field are not missing.
    /// Postcondition:
    /// - Logs error if any field is missing.
    /// </remarks>
    private void CheckLayerRefs()
    {
        if (serializableModel != null)
        {
            model = (M)(IModel)serializableModel;
        }
        if (serializableView != null)
        {
            view = (V)(IView)serializableModel;
        }

        if (model == null)
        {
            Debug.LogError("'model' field is null.");
            Debug.Assert(model != null, "'model' field cannot be null.");
        } else if (view == null)
        {
            Debug.LogError("'view' field is null.");
            Debug.Assert(view != null, "'view' field cannot be null.");
        }
    }

    /// </inheritdoc>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls.
    /// Inherited from MonoBehaviourl.
    /// Verifies all the fields.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - CheckLayerRefs() method is implemented.
    /// - Init() method is implemented.
    /// Postcondition:
    /// - Calls CheckLayerRefs().
    /// - Calls Init() method.
    /// <remarks>
    public virtual void Start()
    {
        CheckLayerRefs();
        Init();
    }
}