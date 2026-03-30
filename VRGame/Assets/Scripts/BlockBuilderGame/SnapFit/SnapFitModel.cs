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
    private float snapRadius = 0.25f;

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
    /// The joint that connects one block to another (when snapped)
    /// </summary>
    private FixedJoint snapJoint = null;

    /// <inheritdoc/>
    public FixedJoint SnapJoint
    {
        get
        {
            return snapJoint;
        }
        set
        {
            snapJoint = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {  
        isSnapped = false;
        snapPoints = new Transform[0];
        snapJoint = null;
        if (snapRadius <= 0)
        {
            snapRadius = 0.25f;
        }
        Assert.IsFalse(isSnapped, "isSnapped must be false on Init");
        Assert.IsNotNull(snapPoints, "SnapPoints must not be null on Init");
        Assert.IsNull(snapJoint, "snapJoint must be null on Init");
        Assert.IsTrue(snapRadius > 0, "snapRadius must be > 0 on Init");
    }
}
