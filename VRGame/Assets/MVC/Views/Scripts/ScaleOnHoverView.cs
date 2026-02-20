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
    // Reference to the controller - using concrete type for Unity serialization
    [SerializeField] private ScaleOnHoverController controller;

    /// <summary>
    /// Initialize the view and validate controller reference
    /// </summary>
    private void Start()
    {
        Init();
        SetupXREvents();
    }

    /// <summary>
    /// Setup XR interaction events for ray interactor hover detection
    /// </summary>
    private void SetupXREvents()
    {
        var xrInteractable = GetComponent<XRBaseInteractable>();
        if (xrInteractable != null)
        {
            xrInteractable.hoverEntered.AddListener(OnXRHoverEnter);
            xrInteractable.hoverExited.AddListener(OnXRHoverExit);
            Debug.Log($"XR hover events connected for {gameObject.name}");
        }
        else
        {
            Debug.LogWarning($"No XRBaseInteractable found on {gameObject.name}. Ray interactor hover won't work!");
        }
    }

    /// <summary>
    /// XR hover enter event handler - called when ray interactor hovers over object
    /// </summary>
    private void OnXRHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log($"XR Hover Enter detected on {gameObject.name}");
        OnHoverEnter();
    }

    /// <summary>
    /// XR hover exit event handler - called when ray interactor stops hovering
    /// </summary>
    private void OnXRHoverExit(HoverExitEventArgs args)
    {
        Debug.Log($"XR Hover Exit detected on {gameObject.name}");
        OnHoverExit();
    }

    /// <summary>
    /// Validates that the controller layer exists
    /// </summary>
    /// Pre-condition:
    ///     -   controller != null
    /// Post-condition:
    ///     -   View holds a reference to the controller 
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
    /// Pre-condition: 
    ///     -   Controller must exist
    /// Post-condition: 
    ///     -   Hover Enter event is processed
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
    /// Pre-condition: 
    ///     -   Controller must exist
    /// Post-condition: 
    ///     -   Hover Exit event is processed
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
    /// Pre-condition:
    ///     -   model != null && linkedObjects exists 
    /// Post-condition:
    ///     -   linkedObjects' scale transitions to its target scale
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
 

