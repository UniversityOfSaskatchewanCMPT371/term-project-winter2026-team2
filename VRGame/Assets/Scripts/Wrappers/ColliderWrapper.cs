
using System;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Wrapper for unity's `Collider` type. Allows functionality involving colliders to be mocked out for unit tests
/// </summary>
public class ColliderWrapper : IColliderWrapper
{
    /// <summary>
    /// Unity Collider object
    /// </summary>
    private Collider collider;

    /// <summary>
    /// Constructor for ColliderWrapper. Simply calls functions from collider.
    /// </summary>
    /// <param name="collider">A Unity collider object</param>
    /// <remarks>
    /// Preconditions
    /// - collider must be non-null
    /// Posconditiosn
    /// - internal collider reference set to input value
    public ColliderWrapper(Collider collider)
    {
        Assert.IsNotNull(collider, "collider must not be null");
        this.collider = collider;
    }

    /// <inheritdoc/>
    public IPlayerController GetPlayerFromParent()
    {
        return collider.GetComponentInParent<IPlayerController>();

    }

    /// <inheritdoc/>
    public bool CompareGameObjectTag(String tag)
    {
        return collider.gameObject.CompareTag(tag);
    }
}