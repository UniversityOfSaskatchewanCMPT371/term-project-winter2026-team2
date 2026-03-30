using UnityEngine;

/// <summary>
/// Interface for the CheckArea Controller.
/// Tracks colliders entering and exiting the check area
/// </summary>
public interface ICheckAreaController : IController
{
    /// <summary>
    /// Resolves model and view references.
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires (modelInstance != null) && (viewInstance != null)
    /// post-condition:
    ///     - ensures controller is ready to track colliders
    /// </remarks>
    new void Init();

    /// <summary>
    /// Adds a collider to the modelInstance collider set
    /// </summary>
    /// <param name="collider">The collider that entered the area</param>
    /// <remarks>
    /// pre-condition:
    ///     - requires collider != null
    /// post-condition:
    ///     - ensures collider is added to InsideColliders
    /// </remarks>
    void OnEnter(Collider collider);

    /// <summary>
    /// Removes a collider from the tracked set.
    /// </summary>
    /// <param name="collider">The collider that exited the area.</param>
    /// <remarks>
    /// post-condition:
    ///     - ensures collider is absent from InsideColliders
    /// </remarks>
    void OnExit(Collider collider);

    /// <summary>
    /// Returns all colliders currently inside the trigger area.
    /// </summary>
    Collider[] GetInside();
}
