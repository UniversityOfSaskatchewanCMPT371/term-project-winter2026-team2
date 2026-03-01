using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ToolTipTrigger : MonoBehaviour, IToolTipTrigger

{
	/// GameObject to be interactable
    public GameObject interactiveElement;
    /// This one is needed so that it can interact with our vr controller ray
    public  XRBaseInteractable interactable;

    /// <inheritdoc> />
    public event Action HoverEntered;
    /// <inheritdoc> />
    public event Action HoverExited;

    private ToolTipController toolTipController;

    /// <summary>
    /// Initializes the component by retrieving the XRBaseInteractable component.
    /// Called by Unity when the script instance is being loaded.
    /// </summary>
    /// <preconditions>
    /// The GameObject must have an XRBaseInteractable component attached (or a derived type).
    /// </preconditions>
    /// <postconditions>
    /// The <c>interactable</c> field is populated with the XRBaseInteractable component
    /// from this GameObject, ready for use in Start() method.
    /// </postconditions>
    /// <exception cref="MissingComponentException">
    /// Thrown by GetComponent if no XRBaseInteractable component is found on the GameObject.
    /// </exception>
    void Awake()
    {
         interactable = GetComponent<XRBaseInteractable>();
 
        
    }

    /// <summary>
    /// Initializes the tooltip trigger by setting up event listeners and creating the controller.
    /// Called before the first frame update by Unity.
    /// </summary>
    /// <remarks>
    /// This method performs the following initialization steps:
    /// <list type="number">
    /// <item>Forwards XR hover events (hoverEntered and hoverExited) to custom events</item>
    /// <item>Creates a new ToolTipController instance with the interactive element and this trigger</item>
    /// </list>
    /// The XR events use lambda expressions to invoke custom events, allowing for additional
    /// subscribers to react to hover interactions without directly coupling to XR Toolkit.
    /// </remarks>
    /// <preconditions>
    /// All serializable fields must be properly assigned:
    /// <list type="bullet">
    /// <item><c>interactiveElement</c> - Must reference a valid GameObject</item>
    /// <item><c>interactable</c> - Must be initialized in Awake() with XRBaseInteractable component</item>
    /// </list>
    /// </preconditions>
    /// <postconditions>
    /// <list type="bullet">
    /// <item>Event listeners are attached to XR interactable hover events</item>
    /// <item>ToolTipController is instantiated and ready to manage tooltip display logic</item>
    /// </list>
    /// </postconditions>
    void Start()
    {
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
    /// </remarks>
    /// <postconditions>
    /// The ToolTipController is disposed and its resources are released.
    /// </postconditions>
    void OnDestroy()
    {
        toolTipController?.Dispose();
       
    }

}
