using UnityEngine;

/// <summary>
/// Model layer base class.
/// Requires internal/external interface.
/// </summary>
public class BaseModel : MonoBehaviour, InternalInterface, ExternalInterface
{
    /*  
    DATA SECTION
    DATA SECTION
    */

    private string prompt = "Hello, from model layer!";

    /*
    EXCLUSIVE METHODS SECTION
    EXCLUSIVE METHODS SECTION
    */
    
    void InternalInterface.ExclusiveMethod()                // ExclusiveMethod() can only be called when casting InternalInterface type on it self.
    {
        Debug.Log(prompt);
    }

    /*
    EXPOSED METHODS SECTION
    EXPOSED METHODS SECTION
    */

    public void ExposedMethod()
    {
        ((InternalInterface)this).ExclusiveMethod();        // Example of calling ExclusiveMethod()
    }
}

/// Model Contract Guidelines
/// - Contains data
/// - Mutates data
/// - No access to View and Controller
/// - Validates input from controller via assertions