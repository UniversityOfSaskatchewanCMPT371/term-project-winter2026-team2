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

    /// <inheritdoc/>
    public void Start()
    {
        Init();
        SetupXREvents();
    }

    /// <inheritdoc/>
    public void SetupXREvents()
    {
        var xrInteractable = GetComponent<XRBaseInteractable>();

        if (xrInteractable == null)
        {
            Debug.LogError("XRBaseInteractable component is required for XR hover events to work");
            return;
        }
        // Assert to ensure XRBaseInteractable is not null
        Assert.IsNotNull(xrInteractable, "XRBaseInteractable component cannot be null for XR hover events");
        
        // All checks passed, setup event listeners for hover enter and exit
        xrInteractable.hoverEntered.AddListener(OnXRHoverEnter);
        xrInteractable.hoverExited.AddListener(OnXRHoverExit);
    }



    /// <inheritdoc/>
    public void OnXRHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log($"XR Hover Enter detected on {gameObject.name}");
        Assert.IsNotNull(controller, "ScaleOnHoverController reference cannot be null in OnXRHoverEnter");
        OnHoverEnter();
    }



    /// <inheritdoc/>
    public void OnXRHoverExit(HoverExitEventArgs args)
    {
        Debug.Log($"XR Hover Exit detected on {gameObject.name}");
        Assert.IsNotNull(controller, "ScaleOnHoverController reference cannot be null in OnXRHoverExit");
        OnHoverExit();
    }



    /// <inheritdoc/>
    public void Init()
    {
        if (controller == null)
        {
            controller = GetComponent<ScaleOnHoverController>();
            
        }
    
        // Assert to ensure controller reference is not null
        Assert.IsNotNull(controller, "ScaleOnHoverController reference cannot be null in the inspector");

        
    }



    /// <inheritdoc/>
    public void OnHoverEnter()
    {
        if (controller == null)
        {
            Debug.LogError("ScaleOnHoverController reference cannot be null");
            return; 
        }
        // Assert to ensure controller reference is not null before processing hover enter
        Assert.IsNotNull(controller, "ScaleOnHoverController reference cannot be null");

        // All checks passed, trigger hover enter event in controller
        controller.OnHoverEnter();
    }



    /// <inheritdoc/>
    public void OnHoverExit()
    {
        if (controller == null)
        {
            Debug.LogError("ScaleOnHoverController reference is null in OnHoverExit");
            return;
        }
        // Assert to ensure controller reference is not null before processing hover exit
        Assert.IsNotNull(controller, "ScaleOnHoverController reference cannot be null in OnHoverExit");

        // All checks passed, trigger hover exit event in controller
        controller.OnHoverExit();
    }



    /// <inheritdoc/>
    public void Update() 
    {
        if (controller == null)
        {
            return; 
        }

        // Grab data from model
        Transform[] linkedObjects = controller.retrieveLinkedObjects();
        Vector3[] targetScales = controller.retrieveTargetScale();
        float scaleSpeed = controller.retrieveScaleSpeed();

        // Additional null checks for safety - return if not initialized
        if (linkedObjects == null || targetScales == null)
        {            
            return; 
        }

        // We use 'Lerp' to gradually move from localScale to targetScale
        for (int i = 0; i < linkedObjects.Length && i < targetScales.Length; i++) {
                // Check if linkedObjects[i] is null
                if (linkedObjects[i] == null) {
                    Debug.LogError("Linked object at index " + i + " is null in Update");
                    continue;
                }

                // Assert to ensure linkedObjects[i] is not null before updating its scale 
                Assert.IsNotNull(linkedObjects[i], "Linked object at index " + i + " cannot be null in Update");

                // All checks passed, update the scale of linkedObjects[i] towards targetScales[i] using Lerp for smooth transition
                linkedObjects[i].localScale = Vector3.Lerp(
                    linkedObjects[i].localScale,
                    targetScales[i],
                    Time.deltaTime * scaleSpeed
                );
        }
    }
}
 

