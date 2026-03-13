using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface defining ToolTipModel for mocking in tests.
/// </summary>
public interface IToolTipModel

{/// <summary>
 /// Gets or sets the title associated with the object.
 /// </summary>
    string Title 
    { 
        /// <summary>
        /// Gets the title value.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the current title string.
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the title value.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` can be any string (including null)
        /// Postconditions:
        /// - the title is updated to the provided value
        /// </remarks>
        set; 
    }
    /// <summary>
    /// Gets or sets the description associated with the object.
    /// </summary>
    string Description 
    { 
        /// <summary>
        /// Gets the description value.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - Returns the current description string.
        /// </remarks>
        get; 
        /// <summary>
        /// Sets the description value.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` can be any string (including null)
        /// Postconditions:
        /// - the description is updated to the provided value
        /// </remarks>
        set; 
    }
}

