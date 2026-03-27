using UnityEngine;

/// <summary>
/// Interface for the ColourButton Model
/// </summary>
public interface IColourModel : IModel
{
    /// <summary>
    /// Array of (colour) materials to cycle through
    /// </summary>
    Material[] Colours
    {
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures return of colours[] of type Material
        /// </remarks>
        get;

        /// <remarks>
        /// pre-condition:  
        ///     - requires (value != null) && (value.Length > 0)
        /// post-condition:
        ///     - ensures materials = value
        /// </remarks>
        set;
    }

    /// <summary>
    /// Current index in the materials cycle.
    /// </summary>
    int CurrentMaterialIndex
    {
        /// <remarks>
        /// pre-condition:
        ///     - requires none
        /// post-condition:
        ///     - ensures currentMaterialIndex is returned
        /// </remarks>
        get;

        /// <remarks>
        /// pre-condition:
        ///     - requires value >= 0
        /// post-condition:
        ///     - ensures currentMaterialIndex = value
        /// </remarks>
        set;
    }

    /// <summary>
    /// Initializes currentMaterialIndex to 0.
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - requires none
    /// post-condition:
    ///     - ensures currentMaterialIndex == 0
    /// </remarks>
    new void Init();
}
