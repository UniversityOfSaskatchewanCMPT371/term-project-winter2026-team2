using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface defining IToolTipView for mocking in tests
/// </summary>
public interface IToolTipView
{
    /// <summary>
    /// Updates the displayed content of the tooltip
    /// </summary>
    /// <param name="model"> The model containing the new title and description</param>
    /// <remarks>
    /// Preconditions:
    /// - `model` must not be null
    /// - The UI components <c>title</c> and <c>description</c> must be assigned non-null values.
    /// Postconditions:
    /// - The title and description text are updated to match the model.
    /// </remarks>
    void UpdateContent(IToolTipModel model);

    /// <summary>
    /// Sets the active state of the tooltip GameObject.
    /// </summary>
    /// <param name="active"> True to show the tooltip, false to hide it. </param>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The tooltip GameObject's active state is set to the value of `active`.
    /// </remarks>
    void SetActive(bool active);
}
