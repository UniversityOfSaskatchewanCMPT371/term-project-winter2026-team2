using UnityEngine;

/// <summary>
/// Model layer base class.
/// Requires internal/external interface.
/// </summary>
public class BaseModel : InternalInterface, ExternalInterface
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
    
    /// <summary>
    /// Example method exclusive to this layer.
    /// </summary>
    void InternalInterface.ExclusiveMethod()                // ExclusiveMethod() can only be called when casting InternalInterface type on it self.
    {
        Debug.Log(prompt);
    }

    /*
    EXPOSED METHODS SECTION
    EXPOSED METHODS SECTION
    */

    /// <summary>
    /// Example method exposed to controller layer.
    /// </summary>
    public void ExposedMethod()
    {
        ((InternalInterface)this).ExclusiveMethod();        // Example of calling ExclusiveMethod()
    }
}