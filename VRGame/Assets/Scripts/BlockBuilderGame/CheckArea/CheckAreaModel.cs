using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

/// <summary>
/// Model component of CheckAreaModel.
/// </summary>
public class CheckAreaModel : Model, ICheckAreaModel
{
    /// <summary>
    /// Reference to the colliders currently inside the check area
    private HashSet<Collider> insideColliders = new HashSet<Collider>();

    /// <inheritdoc/>
    public HashSet<Collider> InsideColliders
    {
        get 
        { 
            return insideColliders; 
        }
        set
        {
            Assert.IsNotNull(value, "InsideColliders value to set must not be null");
            insideColliders = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {
        insideColliders = new HashSet<Collider>();
        Assert.IsNotNull(insideColliders, "insideColliders must not be null on Init");
    }
}
