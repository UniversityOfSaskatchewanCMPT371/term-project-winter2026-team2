using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Base class for controller component.
/// </summary>
/// <remarks>
/// - 'M' is the interface of the model component of your implementation.
/// - 'V' is the interface of the view component of your implementation.
/// These generics are used to define the type of 'modelInstance and viewInstance'
/// that exist in this class for typecasting. Which is needed to access methods
/// from the controller model component and view component.
/// </remarks>
public abstract class Controller<M, V> : MonoBehaviour, IController
    where M : class, IModel
    where V : class, IView
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for model
    /// - Only used by CheckModelRef() to set the value of 'modelInstance' to this field's value
    /// </summary>
    [SerializeField]
    private MonoBehaviour inspectorWindowModel;

    /// <summary>
    /// Reference to the model layer component or
    /// the mock of model layer component. Controller uses data from this.
    /// </summary>
    protected M modelInstance;

    /// <summary>
    /// Internal setter for 'modelInstance' serving as a testhook so that test
    /// assemblies can set a mock model.
    /// </summary>
    internal M ModelMock
    {
        /// <summary>
        /// Set the value of Controller's 'modelInstance' variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' must be non-null
        /// Postconditions:
        /// - Controller's 'modelInstance' variable set to input value
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("'value' passed to set 'modelInstance' is null.");
                // optionally handle gracefully
            }
            Assert.IsNotNull(value, "'value' must not be null.");
            modelInstance = value;
        }
    }

    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for view
    /// - Only used by CheckViewRef() to set the value of 'viewInstance' to this field's value
    /// </summary>
    [SerializeField]
    private MonoBehaviour inspectorWindowView;

    /// <summary>
    /// Reference to the view layer component or
    /// the mock of view layer component. Controller uses methods from this component.
    /// </summary>
    protected V viewInstance;

    /// <summary>
    /// Internal setter for 'viewInstance' serving as a testhook so that test
    /// assemblies can set a mock view.
    /// </summary>
    internal V ViewMock
    {
        /// <summary>
        /// Set the value of Controller's 'viewInstance' variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' must be non-null
        /// Postconditions:
        /// - Controller's 'viewInstance' variable set to input value
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("'value' passed to set 'viewInstance' is null.");
                // optionally handle gracefully
            }
            Assert.IsNotNull(value, "'value' must not be null.");
            viewInstance = value;
        }
    }

    /// <inheritdoc/>
    public void CheckModelRef()
    {
        // first see if the model set in the inspector will work
        if (inspectorWindowModel != null)
        {
            // `as` fails by returning null
            modelInstance = inspectorWindowModel as M;
            if (modelInstance != null)
            {
                return;
            }
            // continue through the other if-case options when null
            Debug.LogWarning($"'inspectorWindowModel' does not implement {typeof(M).Name}.");
        }

        // next see if the model component is attached to the game object
        if (modelInstance == null)
        {
            modelInstance = gameObject.GetComponent<M>();   
        } else
        {
            Debug.Log("'modelInstance' is a mock.");
        }

        // see if model component was found
        if (modelInstance == null)
        {
            Debug.LogWarning($"No matching component implementing {typeof(M).Name} was present.");
            // optionally handle gracefully
        }

        Assert.IsNotNull(modelInstance, "'modelInstance' field cannot be null.");
    }

    /// <inheritdoc/>
    public void CheckViewRef()
    {
        // first see if the  view set in the inspector will work
        if (inspectorWindowView != null)
        {
            // `as` fails by returning null
            viewInstance = inspectorWindowView as V;
            if (viewInstance != null)
            {
                return;
            }
            // continue through the other if-case options when null
            Debug.LogWarning($"'inspectorWindowView' does not implement {typeof(V).Name}.");
        }

        // next see if the view component is attached to the game object
        if (viewInstance == null)
        {
            viewInstance = gameObject.GetComponent<V>();
        } else
        {
            Debug.Log("'viewInstance' is a mock.");
        }

        // see if view component was found
        if (viewInstance == null)
        {
            Debug.LogWarning($"No matching component implementing {typeof(V).Name} was present.");
            // optionally handle gracefully
        }

        Assert.IsNotNull(viewInstance, "'viewInstance' field cannot be null.");
    }

    /// <summary>
    /// Init should call CheckModelRef and CheckViewRef, override and create a
    /// new summary. Pre-conditions and post-conditions should be those of
    /// CheckModelRef and CheckViewRef.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Invokes Init() method.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'Init()' is implemented by the subclass.
    /// - See preconditions of CheckModelRef and CheckViewRef.
    /// Postcondition:
    /// - 'Init()' is invoked.
    /// - See postconditions of CheckModelRef and CheckViewRef.
    /// </remarks>
    public virtual void Start()
    {
        Init();
    }
}