using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View class updates the scale of the linkedObjects 
/// </summary>
public class ScaleOnHoverView : MonoBehaviour, IScaleOnHoverView
{
    // Reference to the controller to access model data and trigger events
    [SerializeField] private ScaleOnHoverController controller;

    /// <summary>
    /// Initialize the view and validate controller reference
    /// </summary>
    /// <pre-condition>
    ///     -   This GameObject must have a ScaleOnHoverController component attached or assigned in the inspector
    /// </pre-condition>
    /// <post-condition>
    ///     -   View is initialized and ready to process hover events
    /// </post-condition>
    private void Start()
    {
        Init();
        SetupXREvents();
    }

    /// <summary>
    /// Setup XR interaction events for ray interactor hover detection
    /// </summary>
    /// <pre-condition>
    ///    -   This GameObject must have an XRBaseInteractable component for ray interaction to work
    /// </pre-condition>
    /// <post-condition>
    ///    -   XR hover events are connected to the appropriate handlers for hover enter and exit
    /// </post-condition>
    private void SetupXREvents()
    {
        var xrInteractable = GetComponent<XRBaseInteractable>();

        /// Check if XRBaseInteractable component exists
        if (xrInteractable == null)
        {
            Debug.LogError("XRBaseInteractable component is required for XR hover events to work");
            return;
        }
        /// Assert to ensure XRBaseInteractable is not null
        Assert.IsNotNull(xrInteractable, "XRBaseInteractable component cannot be null for XR hover events");
        
        /// All checks passed, setup event listeners for hover enter and exit
        xrInteractable.hoverEntered.AddListener(OnXRHoverEnter);
        xrInteractable.hoverExited.AddListener(OnXRHoverExit);
    }

    /// <summary>
    /// XR hover enter event handler - called when ray interactor hovers over object
    /// </summary>
    /// <pre-condition>
    ///     -   Ray interactor must hover over GameObject (with an XRBaseInteractable component)
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverEnter is called to trigger scaling (up) of linkedObjects
    /// </post-condition>
    private void OnXRHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log($"XR Hover Enter detected on {gameObject.name}");
        OnHoverEnter();
    }

    /// <summary>
    /// XR hover exit event handler - called when ray interactor stops hovering
    /// </summary>
    /// <pre-condition>
    ///     -   Ray interactor must stop hovering over GameObject (with an XRBaseInteractable component)
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverExit is called to trigger scaling (down) of linkedObjects
    /// </post-condition>
    private void OnXRHoverExit(HoverExitEventArgs args)
    {
        Debug.Log($"XR Hover Exit detected on {gameObject.name}");
        OnHoverExit();
    }

    /// <summary>
    /// Validates that the controller layer exists
    /// </summary>
    /// <pre-condition>
    ///     -   controller != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   View holds a reference to the controller 
    /// </post-condition>
    public void Init()
    {
        if (controller == null)
        {
            controller = GetComponent<ScaleOnHoverController>();
        }
        Assert.IsNotNull(controller, "Controller cannot be null");
    }


    /// <summary>
    /// Called when hover enters 
    /// </summary>
    /// <pre-condition> 
    ///     -   Controller must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Hover Enter event is processed
    /// </post-condition>
    public void OnHoverEnter()
    {
        if (controller != null)
        {
            controller.OnHoverEnter();
        }
    }

    /// <summary>
    /// Called when hover exits 
    /// </summary>
    /// <pre-condition> 
    ///     -   Controller must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Hover Exit event is processed
    /// </post-condition>
    public void OnHoverExit()
    {
        if (controller != null)
        {
            controller.OnHoverExit();
        }
    }



    /// <summary>
    /// Updates the scaling of linkedObjects
    /// </summary>
    /// <pre-condition>
    ///     -   model != null && linkedObjects exists 
    /// </pre-condition>
    /// <post-condition>
    ///     -   linkedObjects' scale transitions to its target scale
    /// </post-condition>
    public void Update() 
    {
        // Check if controller is null to prevent NullReferenceException
        if (controller == null)
        {
            return;
        }

        // Grab data from model
        Transform[] linkedObjects = controller.retrieveLinkedObjects();
        Vector3[] targetScales = controller.retrieveTargetScale();
        float scaleSpeed = controller.retrieveScaleSpeed();

        // Additional null checks for safety
        if (linkedObjects == null || targetScales == null)
        {
            return;
        }

        // We use 'Lerp' to gradually move from localScale to targetScale
        for (int i = 0; i < linkedObjects.Length && i < targetScales.Length; i++) {
                if (linkedObjects[i] != null) {
                    linkedObjects[i].localScale = Vector3.Lerp(
                    linkedObjects[i].localScale,
                    targetScales[i],
                    Time.deltaTime * scaleSpeed
                );
                }
            }
    }
}
 

