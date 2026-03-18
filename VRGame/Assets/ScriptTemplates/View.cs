using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// MonoBehaviour wrapper. This used specifically for serialized layer fields
/// so that it shows in the inspector that a component requires a 'ViewComponent' instead of 'MonoBehaviour'.
/// </summary>
public class ViewComponent : MonoBehaviour{}

/// <summary>
/// Base class for view component.
/// </summary>
/// <remarks>
/// - 'C' is the interface of the controller component of your implementation.
/// This generic is used to define the type of "controllerInstance" and typecast. Which is needed to access methods
/// from the controller component.
/// </remarks>
public abstract class View<C> : ViewComponent, IView
    where C : class, IController
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for controller
    /// - Only used by CheckControllerRef() to set the value of 'controllerInstance' to this field's value
    /// </summary>
    [SerializeField]
    private ControllerComponent inspectorWindowController;

    /// <summary>
    /// Reference to the controller layer component or
    /// the mock of controller layer component. View uses methods from this.
    /// </summary>
    protected C controllerInstance;

    /// <summary>
    /// Internal setter for 'controllerInstance' serving as a testhook so that test
    /// assemblies can set a mock controller.
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
                // optionally handle gracefully
            }
            Assert.IsNotNull(value, "'value' must not be null.");
            controllerInstance = value;
        }
    }

    /// <inheritdoc/>
    public void CheckControllerRef()
    {
        // first see if the controller set in the inspector will work
        if (inspectorWindowController != null)
        {
            // `as` fails by returning null
            controllerInstance = inspectorWindowController as C;
            if (controllerInstance != null)
            {
                return;
            }
            Debug.LogWarning("'inspectorWindowController' value was not set in inspector.");
        }

        // next see if the controller component is attached to the game object
        if (controllerInstance == null)
        {
            controllerInstance = gameObject.GetComponent<C>();
        } else
        {
            Debug.Log("'viewInstance' is a mock.");
        }

        // see if controller component was found
        if (controllerInstance == null)
        {
            Debug.LogWarning($"No matching component implementing {typeof(C).Name} was present.");
            // optionally handle gracefully
        }

        Assert.IsNotNull(controllerInstance, "'controllerInstance' field cannot be null.");
    }

    /// <summary>
    /// Init should call CheckControllerRef, override and create a
    /// new summary. Pre-conditions and post-conditions should be those of
    /// CheckControllerRef.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Invokes Init() method.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'Init()' is implemented by the subclass.
    /// - See preconditions of CheckControllerRef.
    /// Postcondition:
    /// - 'Init()' is invoked.
    /// - See postconditions of CheckControllerRef.
    /// </remarks>
    public virtual void Start()
    {
        Init();
    }
}