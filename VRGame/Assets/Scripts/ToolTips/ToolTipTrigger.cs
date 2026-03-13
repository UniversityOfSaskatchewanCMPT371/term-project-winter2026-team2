using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

/// <summary>
/// Component that listens to XR hover events and raises custom events for tooltip display.
/// Also creates and manages a ToolTipController to show/hide a tooltip.
/// </summary>
public class ToolTipTrigger : MonoBehaviour, IToolTipTrigger

{
    /// <summary>
	/// GameObject to be interactable that will be shows as the tooltip.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor. This GameObject should contain the tooltip view.
    /// </remarks>
    public GameObject interactiveElement;

    /// <summary>
    /// This one is needed so that it can interact with our vr controller ray
    /// </summary>
    /// <remarks>
    /// Automatacillay obtained via GetComponent in Awake(). Must exist on same GameObject.
    /// </remarks>
    public  XRBaseInteractable interactable;

    /// <inheritdoc/>
    public event Action HoverEntered;
    /// <inheritdoc/>
    public event Action HoverExited;

    private ToolTipController toolTipController;

    /// <summary>
    /// Initializes the component by retrieving the XRBaseInteractable component.
    /// Called by Unity when the script instance is being loaded.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The GameObject must have an XRBaseInteractable component attached (or a derived type).
    /// Postconditions:
    /// - The <c>interactable</c> field is populated with the XRBaseInteractable component
    ///   from this GameObject, ready for use in Start() method.
    /// - <exception cref="MissingComponentException">
    ///   Thrown by GetComponent if no XRBaseInteractable component is found on the GameObject.
    /// </exception>
    /// </remarks>
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        Assert.IsNotNull(interactable, "No XRBaseInteractable component found on this GameObject.");
        
    }

    /// <summary>
    /// Initializes the tooltip trigger by setting up event listeners and creating the controller.
    /// Called before the first frame update by Unity.
    /// </summary>
    /// <remarks>
    /// This method performs the following initialization steps:
    /// 1. Forwards XR hover events (hoverEntered and hoverExited) to custom events
    /// 2. Creates a new ToolTipController instance with the interactive element and this trigger
    /// The XR events use lambda expressions to invoke custom events, allowing for additional
    /// subscribers to react to hover interactions without directly coupling to XR Toolkit.
    /// </remarks>
    /// Preconditions:
    /// All serializable fields must be properly assigned:
    /// - <c>interactiveElement</c> - Must reference a valid GameObject
    /// - <c>interactable</c> - Must be initialized in Awake() with XRBaseInteractable component
    /// Postconditions:
    /// - Event listeners are attached to XR interactable hover events
    /// - ToolTipController is instantiated and ready to manage tooltip display logic
    void Start()
    {
        Assert.IsNotNull(interactiveElement, "interactiveElement must be assigned in the Unity Editor.");
        /// forward XR events to our own events
        interactable.hoverEntered.AddListener(_=> HoverEntered?.Invoke());
        interactable.hoverExited.AddListener(_=> HoverExited?.Invoke());

        /// create controller and pass in the interactive element and this trigger
        toolTipController = new ToolTipController(interactiveElement, this);
        
    }

    /// <summary>
    /// Cleans up resources when the GameObject is destroyed.
    /// Called by Unity when the MonoBehaviour will be destroyed.
    /// </summary>
    /// <remarks>
    /// Ensures proper cleanup of the ToolTipController to prevent memory leaks and
    /// unsubscribe from any events. The null-conditional operator (?.) safely handles
    /// cases where the controller may not have been initialized.
    /// Preconditions:
    /// - None.
    /// Postconditions:
    /// The ToolTipController is disposed and its resources are released.
    /// </remarks>
    void OnDestroy()
    {
        toolTipController?.Dispose();
       
    }

}
