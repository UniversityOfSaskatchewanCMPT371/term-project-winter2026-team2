using UnityEngine;
using UnityEngine.Assertions;
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
        // If not snapped or no snap joint, set snapped to false
        if (!modelInstance.IsSnapped || modelInstance.SnapJoint == null) 
        {
            modelInstance.IsSnapped = false;
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

        foreach (var sf in FindObjectsOfType<SnapFitController>())
        {
            // Only snap to other blocks, not itself
            if (sf == this) 
            {
                continue;
            }

            // Check all snap points of the current block
            foreach (var currentSP in modelInstance.SnapPoints)
            {
                // Check all snap points of the target/other block
                foreach (var targetSP in sf.modelInstance.SnapPoints)
                {
                    // Check if the snap points match
                    bool isMatch = (currentSP.name.StartsWith("Top") && targetSP.name.StartsWith("Bottom")) ||
                                   (currentSP.name.StartsWith("Bottom") && targetSP.name.StartsWith("Top"));

                    // Continue if the snap points don't match
                    if (!isMatch) 
                    {
                        continue;
                    }

                    // Current bottom snap points should only snap to top snap points and vice versa
                    bool grabbedIsAbove = transform.position.y > sf.transform.position.y;
                    bool isDirectionValid = (currentSP.name.StartsWith("Bottom") && targetSP.name.StartsWith("Top") && grabbedIsAbove) ||
                                           (currentSP.name.StartsWith("Top") && targetSP.name.StartsWith("Bottom") && !grabbedIsAbove);
                    if (!isDirectionValid)
                    {
                        continue;
                    }
                    // Calculate the distance between the snap points
                    // If it's greater than the current radius, continue
                    float distance = Vector3.Distance(currentSP.position, targetSP.position);
                    if (distance >= radius) 
                    {
                        continue;
                    }

                    radius = distance;
                    currentSnapPoint = currentSP;
                    otherSnapPoint = targetSP;
                    snapFitController = sf;
                }
            }
        }
        // No valid snap points found within radius, do nothing
        if (snapFitController == null)
        {
            return;
        }

        // Move the block to align the snap points
        transform.position += otherSnapPoint.position - currentSnapPoint.position;

        // Create a joint to connect this block to the target block
        var joint = gameObject.AddComponent<FixedJoint>();

        // Connect the joint to the target block's rigidbody
        joint.connectedBody = snapFitController.GetComponent<Rigidbody>();
        
        // Set the break force and torque of the joint to infinity so it doesn't break under normal conditions
        joint.breakTorque = Mathf.Infinity;
        joint.breakForce = Mathf.Infinity;

        // Set the snap joint in the model to the joint we just created        
        modelInstance.SnapJoint = joint;

        // Disable collision on the joint to prevent physics issues
        joint.enableCollision = false;

        // Set the state to snapped in the model
        modelInstance.IsSnapped = true;
    
    }

    /// </inheritdoc/>
    public bool IsMatch(string name1, string name2)
    {
        Assert.IsNotNull(name1, "name1 must not be null in IsMatch");
        Assert.IsNotNull(name2, "name2 must not be null in IsMatch");
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