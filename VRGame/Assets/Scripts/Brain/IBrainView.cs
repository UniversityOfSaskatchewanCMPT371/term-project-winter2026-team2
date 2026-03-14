/// <summary>
///  Handles XR interaction events and passes them onto the controller
/// </summary>
public interface IBrainView : IView
{
    /// <summary>
    /// Initializes the Brain view component
    /// </summary>
    /// <remarks
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures view is initialized
    /// </remarks>
    new void Init();

    /// <summary>
    /// Sets up XR interaction events on all XRBaseInteractable children
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures eventListeners (OnHoverEnter & OnHoverExit) are attached to all XRBaseInteractable children
    void SetupXREvents();

    /// void OnXRHoverExit(HoverExitEventArgs args);
    /// <summary>
    /// Calls controllerInstance.OnHoverEnter() on XR hover enter 
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures controllerInstance.OnHoverEnter() is called on XR hover enter
    /// </remarks>
    
    /// void OnXRHoverExit(HoverExitEventArgs args);
    /// <summary>
    /// Calls controllerInstance.OnHoverExit() on XR hover exit
    /// </summary>
    /// <remarks>
    /// Pre-condition:
    ///     requires controllerInstance != null
    /// Post-condition:
    ///     ensures controllerInstance.OnHoverExit() is called on XR hover exit
    /// </remarks>

}
