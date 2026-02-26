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
    public Transform pointA;
    
    /// <summary>
    /// The ending point of the line.
    /// </summary>
    public Transform pointB;
    
    /// <summary>
    /// The LineRenderer component used to draw the line.
    /// </summary>
    private LineRenderer line;


    /// <summary>
    /// Initializes the line renderer and validates required references.
    /// </summary>
    /// <preconditions>
    /// <c>pointA</c> and <c>pointB</c> must be assigned in the Unity Editor.
    /// GameObject must have a LineRenderer component attached.
    /// </preconditions>
    /// <postconditions>
    /// The <c>line</c> field is initialized with the LineRenderer component.
    /// </postconditions>
    void Start()

    {
        Assert.IsNotNull(pointA,"Point A must be refrenced");
        Assert.IsNotNull(pointB,"Point B must be refrenced");
        if (pointA == null && pointB == null)
        {
            Debug.LogError("PointA and PointB must be assigned in the Unity Editor.");
            return;
        }
        line = GetComponent<LineRenderer>();

    }

    /// <summary>
    /// Updates the line positions each frame to follow pointA and pointB.
    /// </summary>
    /// <preconditions>
    /// <c>line</c>, <c>pointA</c>, and <c>pointB</c> must be initialized.
    /// </preconditions>
    /// <postconditions>
    /// The LineRenderer displays a line from pointA's position to pointB's position.
    /// </postconditions>
    void Update()
    {
        line.positionCount = 2;
        line.SetPosition(0, pointA.position);
        line.SetPosition(1, pointB.position);

    }
}
