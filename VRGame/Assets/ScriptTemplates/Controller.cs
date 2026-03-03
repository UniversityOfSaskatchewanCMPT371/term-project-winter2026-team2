using Codice.CM.Common;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Base class for controller component.
/// </summary>
/// <remarks>
/// - 'M' is the interface of the model component of your implementation.
/// - 'V' is the interface of the view component of your implementation.
/// These generics are used to define the type of 'modelInstance and viewInstance' and typecast. Which is needed to access methods
/// from the controller model component and view component.
/// </remarks>
public abstract class Controller<M,V> : MonoBehaviour, IController
    where M : class, IModel
    where V : class, IView
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for model
    /// - Only used by Init() to set the value of 'modelInstance' to this field's value
    /// </summary>
    [SerializeField]
    private MonoBehaviour inspectorWindowModel;

    /// <summary>
    /// Reference to the model layer component or
    /// the mock of model layer component. Controller uses data from this.
    /// </summary>
    protected M modelInstance;

    /// <summary>
    /// Internal setter for 'modelInstance' layer field used for mocking.
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
                Assert.IsNotNull(value, "'value' must not be null.");
            }
            modelInstance = value;
        }
    }

    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for view
    /// - Only used by Init() to set the value of 'viewInstance' to this field's value
    /// </summary>
    [SerializeField]
    private MonoBehaviour inspectorWindowView;

    /// <summary>
    /// Reference to the view layer component or
    /// the mock of view layer component. Controller uses methods from this component.
    /// </summary>
    protected V viewInstance;

    /// <summary>
    /// Internal setter for 'viewInstance' field used for mocking.
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
                Assert.IsNotNull(value, "'value' must not be null.");
            }
            viewInstance = value;
        }
    }

    /// <inheritdoc/>
    public void CheckModelRef()
    {
        // if the model component is attached to the game object
        // but 'inspectorWindowModel' value is not set, automatically
        // set it's value
        if (inspectorWindowModel == null && gameObject.GetComponent<M>() != null)
        {
            inspectorWindowModel = gameObject.GetComponent<M>() as MonoBehaviour;
            Debug.LogWarning("'inspectorWindowModel' value was not set in inspector.");
        }
        
        if (inspectorWindowModel != null)
        {
            modelInstance = inspectorWindowModel as M;

            if (modelInstance == null)
            {
                Debug.LogError($"'inspectorWindowModel' does not implement {typeof(M).Name}.");
                Assert.IsNotNull(modelInstance, $"'inspectorWindowModel' needs to implement {typeof(M).Name}.");   
            }
        } 
        
        if (modelInstance == null)
        {
            Debug.LogError("'modelInstance' field is null.");
            Assert.IsNotNull(modelInstance, "'modelInstance' field cannot be null.");
        }
    }

    /// <inheritdoc/>
    public void CheckViewRef()
    {
        // if the view component is attached to the game object
        // but 'inspectorWindowView' value is not set, automatically
        // set it's value
        if (inspectorWindowView == null && gameObject.GetComponent<V>() != null)
        {
            inspectorWindowView = gameObject.GetComponent<V>() as MonoBehaviour;
            Debug.LogWarning("'inspectorWindowView' value was not set in inspector.");
        }

        if (inspectorWindowView != null)
        {
            viewInstance = inspectorWindowView as V;

            if (viewInstance == null)
            {
                Debug.LogError($"'inspectorWindowView' does not implement {typeof(V).Name}.");
                Assert.IsNotNull(viewInstance, $"'inspectorWindowView' needs to implement {typeof(V).Name}.");   
            }
        }

        if (viewInstance == null)
        {
            Debug.LogError("'viewInstance' field is null.");
            Assert.IsNotNull(viewInstance, "'viewInstance' field cannot be null.");
        }
    }

    /// <inheritdoc/>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Invokes Init() method.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'Init()' is implemented by the subclass.
    /// Postcondition:
    /// - 'Init()' is invoked.
    /// </remarks>
    public virtual void Start()
    {
        Init();
    }
}