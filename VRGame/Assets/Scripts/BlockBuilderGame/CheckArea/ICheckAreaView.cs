using UnityEngine;
/// <summary>
/// Interface for the CheckArea View
/// Handles trigger events for the check area and passes them to the controller
/// </summary>
public interface ICheckAreaView : IView
{
    /// <summary>
    /// Initializes insideColliders reference
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires controllerInstance != null
    /// post-condition:
    ///     - ensures controllerInstance is checked
    /// </remarks>
    new void Init();


    
}
