using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// defines the interface for objects that can trigger tooltips.
/// implementing should manage hover events and give
/// references to the interactive element
/// </summary>
public interface IToolTipTrigger
{
    /// <summary>
    /// raised when user starts hovering over the interactive element
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - All subscribers are notified that hover has entered
    /// </remarks>
    event Action HoverEntered;
    
    /// <summary>
    /// raised when user stops hovering over the interactive element
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - All subscribers are notified that hover has exited
    /// </remarks>
    event Action HoverExited;
}
