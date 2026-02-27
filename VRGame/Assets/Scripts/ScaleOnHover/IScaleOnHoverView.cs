using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IScaleOnHoverView
{
    /// <summary>
    /// Initialize the view and validate controller reference
    /// </summary>
    /// <pre-condition>
    ///     -   Must have a ScaleOnHoverController component attached or assigned in the inspector
    /// </pre-condition>
    /// <post-condition>
    ///     -   View is initialized and ready to process hover events
    /// </post-condition>
    void Start();


    /// <summary>
    /// Setup XR interaction events for ray interactor hover detection
    /// </summary>
    /// <pre-condition>
    ///    -   XRBaseInteractable component != null
    /// </pre-condition>
    /// <post-condition>
    ///    -   XR hover events are connected to the appropriate handlers for hover enter and exit
    /// </post-condition>
    void SetupXREvents();


    /// <summary>
    /// XR hover enter event handler - called when ray interactor hovers over object
    /// </summary>
    /// <pre-condition>
    ///     -   Ray interactor must be hovered over GameObject (with an XRBaseInteractable component)
    ///     -   Controller is not null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverEnter is called to trigger scaling (up) of linkedObjects
    /// </post-condition>
    void OnXRHoverEnter(HoverEnterEventArgs args);


    /// <summary>
    /// XR hover exit event handler - called when ray interactor stops hovering
    /// </summary>
    /// <pre-condition>
    ///     -   Ray interactor must stop hovering over GameObject (with an XRBaseInteractable component)
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverExit is called to trigger scaling (down) of linkedObjects
    /// </post-condition>
    void OnXRHoverExit(HoverExitEventArgs args);


    /// <summary>
    /// Validates that the controller layer exists
    /// </summary>
    /// <pre-condition>
    ///     -   controller != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   View holds a reference to the controller 
    /// </post-condition>
    void Init();


    /// <summary>
    /// Called when hover enters 
    /// </summary>
    /// <pre-condition> 
    ///     -   Controller must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Hover Enter event is processed
    /// </post-condition>
    public void OnHoverEnter();


    /// <summary>
    /// Called when hover exits 
    /// </summary>
    /// <pre-condition> 
    ///     -   Controller must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Hover Exit event is processed
    /// </post-condition>
    public void OnHoverExit();


    /// <summary>
    /// Updates the scaling of linkedObjects
    /// </summary>
    /// <pre-condition>
    ///     -   linkedObjects != null 
    ///     -   targetScales != null
    ///     -   scaleSpeed > 0
    /// </pre-condition>
    /// <post-condition>
    ///     -   linkedObjects' scale transitions to its target scale
    /// </post-condition>
    public void Update();
}
