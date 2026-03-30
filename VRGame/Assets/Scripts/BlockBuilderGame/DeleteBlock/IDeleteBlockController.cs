using UnityEngine;
/// <summary>
/// TODO: Change the docstring to match your implementation.
/// </summary>
public interface IDeleteBlockController : IController
{
    /// <summary>
    /// TODO: Change the docstring to match your implementation.
    /// </summary>
    new void Init();

    /// <summary>
    /// Handles the event when a collider enters the block's collider
    /// </summary>
    /// <remarks>
    /// precondition:
    ///     - requires collider != null
    /// postcondition:
    ///     - ensures the game object (associated with the collider) is destroyed
    /// </remarks>
    void HandleColliderEnter(Collider collider);
}
