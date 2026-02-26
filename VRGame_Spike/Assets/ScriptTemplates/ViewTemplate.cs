using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View layer base class.
/// Requires internal/external interface.
/// </summary>
public class BaseView : MonoBehaviour, InternalInterface, ExternalInterface
{
    /*  
    DATA SECTION
    DATA SECTION
    */
    [SerializeField] private BaseController controller;     // Reference to this class's Controller layer

    /*
    EXCLUSIVE METHODS SECTION
    EXCLUSIVE METHODS SECTION
    */

    void InternalInterface.ExclusiveMethod()                // ExclusiveMethod() can only be called when casting InternalInterface type on it self.
    {
        Debug.Log("Hello, from view layer!");
        controller.ExposedMethod();
    }

    /*
    EXPOSED METHODS SECTION
    EXPOSED METHODS SECTION
    */

    public void ExposedMethod()
    {
        ((InternalInterface)this).ExclusiveMethod();        // Example of calling ExclusiveMethod()
    }

    /*
    RUNTIME BEHAVIOURS SECTION
    RUNTIME BEHAVIOURS SECTION
    */

    /// <summary>
    /// Called after the scene loads.
    /// </summary>
    private void Awake()
    {
        // Verify layer references before starting
        Assert.IsNotNull(controller, "Reference to controller layer cannot be null.");
        if (controller == null) return;

        ExposedMethod();
        // Your code here
    }
}

/// View Contract Guidelines
/// - Contains object references
/// - Access to Controller
/// - No access to Model (only through controller)
/// - Validates object/layer references via assertions