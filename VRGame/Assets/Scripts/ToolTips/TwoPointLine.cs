using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// Draws a line between two points using a LineRenderer.
/// </summary>
public class TwoPointLine : MonoBehaviour
{
    /// <summary>
    /// The starting point of the line.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor.
    /// </remarks>
    public Transform pointA;
    
    /// <summary>
    /// The ending point of the line.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor.
    /// </remarks>
    public Transform pointB;
    
    /// <summary>
    /// The LineRenderer component used to draw the line.
    /// </summary>
    private LineRenderer line;


    /// <summary>
    /// Initializes the line renderer and validates required references.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - <c>pointA</c> and <c>pointB</c> must be assigned in the Unity Editor.
    /// - The GameObject must have a LineRenderer component attached.
    /// </remarks>
    /// Postconditions:
    /// - The <c>line</c> field is initialized with the LineRenderer component.
    /// - If any precondition is not met, an assertion fails and an error is logged.
    /// </remarks>
    void Start()

    {
        // Validate that pointA and pointB are assigned
        if (pointA == null)
        {
            Debug.LogError("PointA must be assigned in the Unity Editor.");
            Assert.IsNotNull(pointA,"PointA cannot be null.");
        }
        if (pointB == null)
        {
            Debug.LogError("PointB must be assigned in the Unity Editor.");
            Assert.IsNotNull(pointB,"PointB cannot be null.");
        }

        line = GetComponent<LineRenderer>();
        Assert.IsNotNull(line, "No LineRenderer component found on this GameObject.");

    }

    /// <summary>
    /// Updates the line positions each frame to follow pointA and pointB.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - <c>line</c>, <c>pointA</c>, and <c>pointB</c> must be initialized
    /// - If any reference is missing, the method does nothing
    /// Postconditions:
    /// - The LineRenderer displays a line from pointA's position to pointB's position.
    /// </remarks>
    void Update()
    {
        if (line == null || pointA == null || pointB == null)
        {
            // If any required reference is missing, log an error and skip updating the line
            Debug.LogError("Cannot update line positions. Ensure that LineRenderer, PointA, and PointB are all assigned.");
            return;
        }
        
        line.positionCount = 2;
        line.SetPosition(0, pointA.position);
        line.SetPosition(1, pointB.position);

    }
}
