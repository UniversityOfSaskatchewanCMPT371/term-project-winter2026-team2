/// <summary>
/// Interface for the RotateButton View.
/// Handles wiring XR button events to the controller.
/// </summary>
public interface IRotateButtonView : IView
{
    /// <summary>
    /// Checks for controller reference and calls SetupXREvents.
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-conditions:
    ///     - ensures (XR events are initialized) && (SetUpXREvents() is invoked)
    /// </remarks>
    new void Init();

    /// <summary>
    /// Sets up XR interactable listeners on XR Base Interactable components
    /// </summary>
    /// <remarks>
    /// pre-conditions:
    ///     - requires (controllerInstance != null) and
    ///                 (at least one XRBaseInteractable child must exist)
    /// post-conditions:
    ///     - ensures selectEntered listeners are registered on all child XRBaseInteractables
    /// </remarks>
    void SetupXREvents();
}
