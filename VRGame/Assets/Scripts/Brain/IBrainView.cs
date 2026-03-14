/// <summary>
///  Handles XR interaction events and passes them onto the controller
/// </summary>
public interface IBrainView : IView
{
    /// <summary>
    /// Initializes the Brain view component
    /// </summary>
    new void Init();

    /// <summary>
    /// Method sets up XR interaction events
    /// </summary>
    void setupXREvents();

    /// <summary>
    /// Calls controllerInstance.OnHoverEnter() on XR hover enter 
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures controllerInstance.OnHoverEnter() is called on XR hover enter
    /// </remarks>
    void OnXRHoverEnter(HoverEnterEventArgs args);

    /// <summary>
    /// Calls controllerInstance.OnHoverExit() on XR hover exit
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures controllerInstance.OnHoverExit() is called on XR hover exit
    /// </remarks>
    void OnXRHoverExit(HoverExitEventArgs args);
}
