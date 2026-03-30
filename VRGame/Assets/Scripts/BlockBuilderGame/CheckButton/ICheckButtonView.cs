/// <summary>
/// Interface for the CheckButton View.
/// Sets up XR event listeners for button press 
/// </summary>
public interface ICheckButtonView : IView
{
    /// <summary>
    /// Resolves controller reference and registers XR button press events.
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-condition:
    ///     - ensures SetupXREvents() is called
    /// </remarks>
    new void Init();

    /// <summary>
    /// Sets up XR interaction events on child interactable components
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-condition:
    ///     - ensures XR selectEntered events are added on all child XRBaseInteractable components
    void SetupXREvents();
}
