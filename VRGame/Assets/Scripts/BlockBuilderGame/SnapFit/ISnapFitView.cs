using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Interface for the SnapFit View.
/// Sets up listeners for grab and release events to trigger snapping and detaching in the controller.
/// </summary>
public interface ISnapFitView : IView
{
    /// <summary>
    /// Checks controller reference and registers XR release listener
    /// </summary>
    /// <remarks>
    /// pre-condition:  
    ///     - requires (controllerInstance != null) && (grab != null)
    /// post-condition: 
    ///     - ensures listers are added to grab interactable components
    /// </remarks>
    new void Init();

    /// <summary>
    /// Listener for grab event, triggers detach in controller
    /// </summary> 
    /// <remarks>
    /// pre-condition:
    ///     - requires (controllerInstance != null) && (grab != null)
    /// post-condition:
    ///     - ensures (Detach() is invoked) && (block is reset to upright orientation)
    /// </remarks>
    void OnGrabbed(SelectEnterEventArgs args);

    /// <summary>
    /// Listener for release event, triggers snap in controller
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (controllerInstance != null) && (grab != null)
    /// post-condition:
    ///     - ensures Snap() is invoked (in controller component)
    /// </remarks>
    void OnReleased(SelectExitEventArgs args);

    /// <summary>
    /// Unregisters listeners on destroy
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (grab != null)
    /// post-condition:
    ///     - ensures listeners are removed from grab interactable components
    /// </remarks>
    void OnDestroy();
}
