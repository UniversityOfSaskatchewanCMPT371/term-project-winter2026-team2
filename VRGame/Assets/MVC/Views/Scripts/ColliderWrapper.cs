
using System;
using UnityEditor.VersionControl;
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

    /// <summary>
    /// Retrieve `IPlayerController` component if present in parent collider
    /// </summary>
    /// <returns>IPlayerController component present in parent collider</returns>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - IplayerController component in collider's parent returned if present, NULL otherwise
    /// </remarks>
    public IPlayerController GetPlayerFromParent()
    {
        return collider.GetComponentInParent<IPlayerController>();

    }

    public bool CompareGameObjectTag(String tag)
    {
        return collider.gameObject.CompareTag(tag);
    }
}