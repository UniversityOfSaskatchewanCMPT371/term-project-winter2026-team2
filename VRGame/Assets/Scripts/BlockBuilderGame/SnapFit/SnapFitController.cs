using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

/// <summary>
/// Controller component of SnapFitController.
/// </summary>
public class SnapFitController : Controller<ISnapFitModel, ISnapFitView>, ISnapFitController
{
    /// <summary>
    /// Initialize references and snap points on startup
    /// </summary>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Init()
    {
        // Check references
        this.CheckModelRef();
        this.CheckViewRef();

        // Find snap points of the block prefab 
        FindSnapPoints();
    }

    /// <inheritdoc/>
    public void FindSnapPoints()
    {
        // List to hold snap points
        var points = new List<Transform>();
        foreach (Transform component in GetComponentsInChildren<Transform>())
        {
            // Find snap points of the block prefab by looking for components named "Top" or "Bottom"
            if (component.name.StartsWith("Top") || component.name.StartsWith("Bottom"))
            {
                points.Add(component);
            }
        }

        // Set snap points in model
        modelInstance.SnapPoints = points.ToArray();
        if (modelInstance.SnapPoints.Length == 0)
        {
            Debug.LogWarning("No snap points found for SnapFitController");
        }
    }

    /// <inheritdoc/>
    public void Detach()
    {
        modelInstance.IsSnapped = false;
    }

    /// <inheritdoc/>
    public void Snap()
    {
        modelInstance.IsSnapped = true;
    }

    public void FindSnapTarget()
    {
        // Get snap radius from model
        float radius = modelInstance.SnapRadius;

        // The snap points of the current block and the block we are trying to snap to
        Transform currentSnapPoint = null;
        Transform otherSnapPoint = null;

        // The SnapFitController of the block we are trying to snap to
        SnapFitController snapFitController = null;

        // Find all other SnapFitControllers in the scene
        var controllers = FindObjectsByType<SnapFitController>();

        foreach (var controller in controllers)
        {
            if (controller == this) continue;

            // Only snap to blocks that are already placed
            if (!controller.modelInstance.IsSnapped) 
            {
                continue;
            }

            // Check all snap points of the current block
            foreach (var currentSP in modelInstance.SnapPoints)
            {
                // Check all snap points of the target/other block
                foreach (var targetSP in controller.modelInstance.SnapPoints)
                {
                    // Only snap to snap points with matching names 
                    if (!IsMatch(currentSP.name, targetSP.name)) 
                    {
                        continue;
                    }

                    // Check distance between snap points
                    float distance = Vector3.Distance(currentSP.position, targetSP.position);
                    
                    // If distance is within radius and is closest, set as snap target
                    if (distance < radius)
                    {
                        radius = distance;
                        currentSnapPoint = currentSP;
                        otherSnapPoint = targetSP;
                        snapFitController = controller;
                    }
                }
            }
        }
        // Check if we found a snap target 
        if (currentSnapPoint != null && otherSnapPoint != null)
        {
            // Calculate the offset between the snap points
            Vector3 offset = otherSnapPoint.position - currentSnapPoint.position;

            // Move the block into position to snap the snap points together
            transform.position += offset;

            // Change state to snapped
            modelInstance.IsSnapped = true;
        }
    }

    /// </inheritdoc/>
    public bool IsMatch(string name1, string name2)
    {
        // Check if the snap point names match
        if ((name1.StartsWith("Top") && name2.StartsWith("Bottom")) || 
            (name1.StartsWith("Bottom") && name2.StartsWith("Top")))
        {
            return true;
        }
        return false;
    }
}