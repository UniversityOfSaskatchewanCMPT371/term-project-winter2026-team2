using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of SnapFitModel.
/// </summary>
public class SnapFitModel : Model, ISnapFitModel
{
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

    /// <inheritdoc/>
    public override void Init()
    {

    }
}
