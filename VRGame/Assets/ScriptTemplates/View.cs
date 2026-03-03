using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Base class for view component.
/// </summary>
/// <remarks>
/// - 'C' is the interface of the controller component of your implementation.
/// This generic is used to define the type of "controllerInstance" and typecast. Which is needed to access methods
/// from the controller component.
/// </remarks>
public abstract class View<C> : MonoBehaviour, IView
    where C : class, IController
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for controller
    /// - Only used by Init() to set the value of 'controllerInstance' to this field's value
    /// </summary>
    [SerializeField]
    private MonoBehaviour inspectorWindowController;

    /// <summary>
    /// Reference to the controller layer component or
    /// the mock of controller layer component. View uses methods from this.
    /// </summary>
    protected C controllerInstance;

    /// <summary>
    /// Internal setter for 'controllerInstance' layer field used for mocking.
    /// </summary>
    internal C ControllerMock
    {
        /// <summary>
        /// Set the value of View's 'controllerInstance' variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' must be non-null
        /// Postconditions:
        /// - View's 'controllerInstance' variable set to input value
        set
        {
            if (value == null)
            {
                Debug.LogError("'value' passed to set 'controllerInstance' is null.");
                Assert.IsNotNull(value, "'value' must not be null.");
            }
            controllerInstance = value;
        }
    }

    /// <inheritdoc/>
    public void CheckControllerRef()
    {
        // if the controller component is attached to the game object
        // but 'inspectorWindowController' value is not set, automatically
        // set it's value
        if (inspectorWindowController == null && gameObject.GetComponent<C>() != null)
        {
            inspectorWindowController = gameObject.GetComponent<C>() as MonoBehaviour;
            Debug.LogWarning("'inspectorWindowController' value was not set in inspector.");
        }

        if (inspectorWindowController != null)
        {
            controllerInstance = inspectorWindowController as C;

            if (controllerInstance == null)
            {
                Debug.LogError($"'inspectorWindowController' does not implement {typeof(C).Name}.");
                Assert.IsNotNull(controllerInstance, $"'inspectorWindowController' needs to implement {typeof(C).Name}.");   
            }
        }

        if (controllerInstance == null)
        {
            Debug.LogError("'controllerInstance' field is null.");
            Assert.IsNotNull(controllerInstance, "'controllerInstance' field cannot be null.");
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