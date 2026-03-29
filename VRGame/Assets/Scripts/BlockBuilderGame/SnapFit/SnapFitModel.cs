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
    
    
    /// <inheritdoc/>
    public override void Init()
    {

    }
}
