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
}