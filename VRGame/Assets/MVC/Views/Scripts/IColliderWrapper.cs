
using System;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Wrapper for unity's `Collider` type. Allows functionality involving colliders to be mocked out for unit tests
/// </summary>
public interface IColliderWrapper
{
    /// <summary>
    /// Constructor for ColliderWrapper. Simply calls functions from collider.
    /// </summary>
    /// <param name="collider">A Unity collider object</param>
    /// <remarks>
    /// Preconditions
    /// - collider must be non-null
    /// Posconditiosn
    /// - internal collider reference set to input value
    
    // You can't have constructor signatures in interfaces for some reason
    //ColliderWrapper(Collider collider);

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
    IPlayerController GetPlayerFromParent();

    /// <summary>
    /// Compares the collider's gameObject.tag to the input tag
    /// </summary>
    /// <param name="tag">tag to compare the collider's gameObject.tag to</param>
    /// <returns>true if the same, false otherwise</returns>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Either true or false returned depending on whether tags strings are the same
    /// </remarks>
    bool CompareGameObjectTag(string tag);
}