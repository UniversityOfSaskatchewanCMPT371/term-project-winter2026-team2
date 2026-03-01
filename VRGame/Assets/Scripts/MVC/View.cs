using UnityEngine;

/// <summary>
/// Base class for view component.
/// </summary>
public abstract class View : MonoBehaviour, IView
{
    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for controller
    /// </summary>
    private Controller serializableController;

    /// <summary>
    /// Controller portion of door module. Controller portion uses data from this
    /// </summary>
    private IController controller;

    /// <summary>
    /// Public accessor of controller portion of door module
    /// </summary>
    internal IController Controller
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