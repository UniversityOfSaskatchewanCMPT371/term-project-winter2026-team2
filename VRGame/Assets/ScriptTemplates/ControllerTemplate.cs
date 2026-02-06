using UnityEngine;

/// <summary>
/// Controller layer base class.
/// Requires internal/external interface.
/// </summary>
public class BaseController : InternalInterface, ExternalInterface
{
    /*  
    DATA SECTION
    DATA SECTION
    */
    private BaseModel model = new BaseModel();
    private BaseView view = new BaseView();

    /*
    EXCLUSIVE METHODS SECTION
    EXCLUSIVE METHODS SECTION
    */

    void InternalInterface.ExclusiveMethod()
    {
        Debug.Log("Hello, from controller layer!");
        model.ExposedMethod();
    }

    /*
    EXPOSED METHODS SECTION
    EXPOSED METHODS SECTION
    */

    public void ExposedMethod()
    {
        ((InternalInterface)this).ExclusiveMethod();
    }
}