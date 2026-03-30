using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Interface for the CheckArea Model.
/// Stores the set of colliders currently inside the trigger area.
/// </summary>
public interface ICheckAreaModel : IModel
{
    /// <summary>
    /// Initializes the model state.
    /// </summary>
    /// <remarks>
    /// post-condition:
    ///     - ensures InsideColliders is an empty set
    /// </remarks>
    new void Init();

    /// <summary>
    /// The set of colliders currently inside the trigger area.
    /// </summary>
    HashSet<Collider> InsideColliders { get; }
}
