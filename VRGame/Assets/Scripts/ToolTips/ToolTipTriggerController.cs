using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controls the visibility of a tooltip based on hover events
/// Raised by ToolTipTrigger.
/// </summary>
public class ToolTipController
{
    /// <summary>
    /// The GameObject that displays the tooltip content.
    /// </summary>
    private readonly GameObject interactiveElement;
    
    /// <summary>
    /// The trigger that raises hover events.
    /// </summary>
    private readonly IToolTipTrigger trigger;

    /// <summary>
    /// Initializes a new instance of the ToolTipController class.
    /// </summary>
    /// <param name="interactiveElement">The GameObject to show/hide as a tooltip.</param>
    /// <param name="trigger">The trigger that provides hover events.</param>
    /// <remarks>
    /// Preconditions:
    /// - `interactiveElement` must not be null
    /// - `trigger` must not be null
    /// Postconditions:
    /// - Event handlers are subscribed to trigger events
    /// - the interactive element is initialliy hidden (SetActive(false)).
    /// </remarks>
    public ToolTipController(GameObject interactiveElement, IToolTipTrigger trigger)
    {
        if (interactiveElement == null)
        {
            Debug.LogError("interactiveElement cannot be null.");
            Debug.Assert(interactiveElement != null, "interactiveElement cannot be null.");
            return;
        }

        if (trigger == null)
        {
            Debug.LogError("trigger cannot be null.");
            Debug.Assert(trigger != null, "trigger cannot be null.");
            return;
        }

        this.interactiveElement = interactiveElement;
        this.trigger = trigger;

        //subscribe to the trigger events
        trigger.HoverEntered += OnHoverEnter;
        trigger.HoverExited += OnHoverExit;

        //start with the interactive element hidden
        interactiveElement.SetActive(false);
    }

    /// <summary>
    /// Shows the tooltip when hover enters.
    /// </summary>
    /// <remark>
    /// Preconditions:
    /// - `interactiveElement` must be initialized.
    /// Postconditions:
    /// - The interactive element is visible and active.
    /// </remark>
    private void OnHoverEnter()
    {
        //null check
        if (interactiveElement == null)
        {
            Debug.LogError("OnHoverEnter called but interactiveElement is null.");
        }
        interactiveElement.SetActive(true);
    }

    /// <summary>
    /// Hides the tooltip when hover exits.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `interactiveElement` must be initialized.
    /// Postconditions:
    /// - The interactive element is hidden and inactive.
    /// </remarks>
    private void OnHoverExit()
    {
        //null check
        if (interactiveElement == null)
        {
            Debug.LogError("OnHoverExit called but interactiveElement is null.");
        }
        interactiveElement.SetActive(false);
    }

    /// <summary>
    /// Cleans up event subscriptions to prevent memory leaks.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Event handlers must be subscribed to trigger events.
    /// Postconditions:
    /// - All event handlers are unsubscribed from trigger events.
    /// </remarks>
    public void Dispose()
    {
        if (trigger != null)
        {
            //unsubscribe to prevent memory leaks
            //unsubscribing is safe even if it were never subscribed (does nothing)
            trigger.HoverEntered -= OnHoverEnter;
            trigger.HoverExited -= OnHoverExit;
        }
       
    }
}
