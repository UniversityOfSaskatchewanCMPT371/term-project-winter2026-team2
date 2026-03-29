using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of SnapFitModel.
/// </summary>
public class SnapFitModel : Model, ISnapFitModel
{
    /// <summary>
    /// The snap points where blocks snap together
    private Transform[] snapPoints = new Transform[0];

    /// <inheritdoc/>
    public Transform[] SnapPoints
    {
        get
        {
            return snapPoints;
        }
        set
        {
            Assert.IsNotNull(value, "SnapPoint value must not be null");
            snapPoints = value;
        }
    }

    /// <summary>
     /// The radius in which the snap points will trigger snap
     /// </summary>
    private float snapRadius = 0.5f;

    /// <inheritdoc/>
    public float SnapRadius
    {
        get
        {
            return snapRadius;
        }
        set
        {
            Assert.IsTrue(value > 0, "SnapRadius value must be greater than 0");
            snapRadius = value;
        }
    }
    
    /// <summary>
    /// The state of whether this block is currently snapped to another block
    /// </summary>
    private bool isSnapped = false;

    /// <inheritdoc/>
    public bool IsSnapped
    {
        get
        {
            return isSnapped;
        }
        set
        {
            isSnapped = value;
        }
    }

    /// <summary>
    /// The point that connects one block to another (when snapped)
    /// </summary>
    private FixedJoint snapPoint = null;

    /// <inheritdoc/>
    public FixedJoint SnapPoint
    {
        get
        {
            return snapPoint;
        }
        set
        {
            snapPoint = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {

    }
}
