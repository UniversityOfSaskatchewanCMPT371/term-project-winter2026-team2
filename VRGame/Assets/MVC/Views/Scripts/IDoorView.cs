

using UnityEngine;
/// <summary>
/// View portion of the reusable door module. Collisions are handled here
/// </summary>
/// <remarks>
/// - doorController is always non-null upon calling Init()
/// </remarks>
public interface IDoorView
{

    /// <summary>
    /// Initializes the DoorView. Called by the game within the
    /// MonoBehaviour function `Start()` (executes the frame a script is enabled)
    /// - Separated from `Start()`, as this makes unit testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `doorController` instance var must be non-null
    /// PostConditions:
    /// - Checked to make sure DoorView will be able to function properly, has necessary values set for instance vars
    /// </remarks>
    public void Init();


    /// <summary>
    /// Called when another object's collider enters this door's collider.
    /// Handles player collision with door.
    /// </summary>
    /// <param name="other">Collider than has interacted with this door's collider</param>
    /// <remarks>
    /// Preconditions:
    /// - `Collider other` must be non-null
    /// - `doorController` instance var must be non-null
    /// Postconditions:
    /// - changes to state created by calling `doorController.OnPlayerEnter()` with colliders
    /// associated palyer controller
    public void OnTriggerEnter(Collider other);

    /// <summary>
    /// Actual logic for OnTriggerEnter functionality. Separated to make unit testing easier
    /// - collider's cannot be mocked out, so the actual OnTriggerEnter() puts the collider
    /// in a wrapper class and should call this
    /// </summary>
    /// <param name="colliderWrapper">colliderWrapper created within OnTriggerEnter()</param>
    /// <remarks>
    /// PreConditions:
    /// - colliderWrapper must not be null
    /// PostConditions:
    /// - changes to state created by calling doorController.OnPlayerEnter()` with collider's
    /// associated PlayerController
    public void OnTriggerEnterLogic(IColliderWrapper colliderWrapper);
}