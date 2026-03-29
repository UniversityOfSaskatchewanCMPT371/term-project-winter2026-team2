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

        // Find snap points of the block prefab by looking for components named "Top" or "Bottom"
        var points = new List<Transform>();
        foreach (Transform component in GetComponentsInChildren<Transform>())
            if (component.name.StartsWith("Top") || component.name.StartsWith("Bottom"))
                points.Add(component);

        // Set snap points in model
        modelInstance.SnapPoints = points.ToArray();
        if (modelInstance.SnapPoints.Length == 0)
            Debug.LogError("No snap points found for SnapFitController");
    }

    /// <inheritdoc/>
    public void Detach()
    {
        // If not snapped or no snap joint, do nothing
        if (!modelInstance.IsSnapped || modelInstance.SnapJoint == null) 
        {
            return;
        }
        // Destroy the joint connecting this block to another block
        Destroy(modelInstance.SnapJoint);
        modelInstance.SnapJoint = null;
        // Change state to not snapped
        modelInstance.IsSnapped = false;
    }

    /// <inheritdoc/>
    public void Snap()
    {
        modelInstance.IsSnapped = true;
        FindSnapTarget();
    }

    public void FindSnapTarget()
    {
        // Check if already snapped, if so do nothing
        if (modelInstance.IsSnapped) 
        {
            return;
        }

        // Get snap radius from model
        float radius = modelInstance.SnapRadius;

        // The snap points of the current block and the block we are trying to snap to
        Transform currentSnapPoint = null;
        Transform otherSnapPoint = null;

        // The SnapFitController of the block we are trying to snap to
        SnapFitController snapFitController = null;

        // Find all other SnapFitControllers in the scene
        var snapFitControllers = FindObjectsByType<SnapFitController>();

        foreach (var sf in FindObjectsOfType<SnapFitController>())
        {
            // Check if the current SnapFitController is not this block and is already snapped to another block
            if (sf == this || !sf.modelInstance.IsSnapped) 
            {
                continue;
            }

            // Check all snap points of the current block
            foreach (var currentSP in modelInstance.SnapPoints)
            {
                // Check all snap points of the target/other block
                foreach (var targetSP in sf.modelInstance.SnapPoints)
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
                        snapFitController = sf;
                    }
                }
            }
        }
        // Change state to snapped
        modelInstance.IsSnapped = true;

        // Update position of this block to match the snap point of the target block
        transform.position += otherSnapPoint.position - currentSnapPoint.position;

        // Create a joint to connect this block to the target block
        var joint = gameObject.AddComponent<FixedJoint>();

        // Connect the joint to the target block's rigidbody
        joint.connectedBody = snapFitController.GetComponent<Rigidbody>();
        // Set the break force and torque of the joint to infinity so it doesn't break under normal conditions
        joint.breakForce = joint.breakTorque = Mathf.Infinity;
        // Set the snap joint in the model to the joint we just created
        modelInstance.SnapJoint = joint;
    
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

    /// <inheritdoc/>
    private void OnJointBreak()
    {
        modelInstance.SnapJoint = null;
        modelInstance.IsSnapped = false;
    }
}