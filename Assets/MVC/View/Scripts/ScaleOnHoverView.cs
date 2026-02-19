using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View class updates the scale of the linkedObjects 
public class ScaleOnHoverView : IScaleOnHoverView
{
    // Reference to the controller
    [SerializeField] private IScaleOnHoverController controller;

    /// <summary>
    /// Validates that the controller layer exists
    /// </summary>
    /// Pre-condition:
    ///     -   controller != null
    /// Post-condition:
    ///     -   View holds a reference to the controller 
    public void Init()
    {
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
        // Grab data from model
        Transform[] linkedObjects = controller.retrieveLinkedObjects();
        Vector3[] targetScales = controller.retrieveTargetScale();
        float scaleSpeed = controller.retrieveScaleSpeed();

        // We use 'Lerp' to gradually move from localScale to targetScale
        for (int i = 0; i < linkedObjects.Length; i++) {
            linkedObjects[i].localScale = Vector3.Lerp(
                linkedObjects[i].localScale,
                targetScales[i],
                deltaTime * speed
            );
        }
    }
}
