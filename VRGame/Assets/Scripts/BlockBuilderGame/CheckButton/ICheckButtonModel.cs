using UnityEngine;
/// <summary>
/// Interface for the CheckButton Model.
/// Holds the reference to the CheckArea controller.
/// </summary>
public interface ICheckButtonModel : IModel
{
    /// <summary>
    /// Initializes the model state
    /// </summary>
    new void Init();

    /// <summary>
    /// Reference to the CheckArea's scanner animator
    /// </summary>
    Animator Scanner 
    { 
        /// <summary>
        /// Returns the reference to the CheckArea's scanner animator
        /// </summary>
        /// <remarks>
        /// precondition:
        ///     - requires none
        /// </remarks>
        /// postcondition:
        ///     - ensures this.scanner is returned 
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the reference to the CheckArea's scanner animator
        /// </summary>
        /// <remarks>
        /// precondition:
        ///    - requires value != null
        /// postcondition:
        ///    - ensures this.scanner == value
        /// </remarks>
        set;
    }

}
