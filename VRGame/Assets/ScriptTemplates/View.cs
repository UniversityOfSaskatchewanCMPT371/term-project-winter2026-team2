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

    /// <summary>
    /// Called by Start() at runtime. Resolves and validates the controller
    /// reference required by this component.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - The GameObject may or may not have a component implementing the
    ///   generic interface. If present, it can be
    ///   auto‑assigned when the inspector field is unset.
    ///
    /// Postcondition:
    /// - 'controllerInstance' is assigned to a component implementing
    ///   <typeparamref name="C"/>.
    /// - Logs errors and raises assertions when the resolved reference is
    ///   missing or does not implement <typeparamref name="C"/>.
    /// </remarks>
    private void CheckLayerRefs()
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

    /// <inheritdoc></inheritdoc>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Ensures that this component's
    /// controller reference is resolved and validated before initialization.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'CheckLayerRefs()' is implemented and able to resolve the
    ///   controller reference.
    /// - 'Init()' is implemented by the subclass.
    ///
    /// Postcondition:
    /// - 'CheckLayerRefs()' is invoked to resolve and validate
    ///   'controllerInstance'.
    /// - 'Init()' is invoked after a valid controller reference is ensured.
    /// - Errors and assertions occur if the controller reference cannot be resolved.
    /// </remarks>
    public virtual void Start()
    {
        CheckLayerRefs();
        Init();
    }
}