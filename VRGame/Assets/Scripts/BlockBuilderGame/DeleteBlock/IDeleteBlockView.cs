/// <summary>
/// Interface for the view component of DeleteBlock.
/// Handles the OnColliderEnter event and passes it to the controller.
/// </summary>
public interface IDeleteBlockView : IView
{
    /// <summary>
    /// TODO: Change the docstring to match your implementation.
    /// </summary>
    new void Init();

    /// <summary>
    /// Calls the controller's HandleColliderEnter method when a collider enters the block's collider
    /// </summary>
    /// <remarks>
    /// precondition:
    ///     - requires collider != null
    /// postcondition:
    ///     - ensures controllerInstance.HandleColliderEnter(collider) is called
    /// </remarks>
    void OnColliderEnter(Collider collider);
}
