using UnityEngine;

/// <summary>
/// Base manger class.
/// Requires internal/external interface.
/// </summary>
public class BaseManager : InternalInterface, ExternalInterface
{

    /*
    DATA SECTION
    DATA SECTION
    */
    private string prompt = "Hello, from manager!";

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

/// Manager Contract Guidelines
/// - Governs the game (ex. SceneChanger, or AudioManager)
/// - Contains data (some global)
/// - Mutates data
/// - Not an MVC, but a service