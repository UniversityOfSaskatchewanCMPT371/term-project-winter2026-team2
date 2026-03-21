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
    /// Track the currently visible tooltip.
    private static GameObject activeTooltip;
    /// </summary>

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
        Debug.Assert(!interactiveElement.activeSelf, "Interactive element should be inactive after initialization.");
    }

    /// <summary>
    /// Shows the tooltip when hover enters.
    /// </summary>
    /// <remark>
    /// Preconditions:
    /// - `interactiveElement` must be initialized.
    /// Postconditions:
    /// - The interactive element is visible and active.
    /// - Any previously visible tooltip is hidden.
    /// </remark>
    private void OnHoverEnter()
    {
        //null check
        if (interactiveElement == null)
        {
            Debug.LogError("OnHoverEnter called but interactiveElement is null.");
        }
        // Hide the previous active tooltip if it's different
        if (activeTooltip != null && activeTooltip != interactiveElement)
        {
            activeTooltip.SetActive(false);
        }

        // Show the new tooltip and remember it as active
        interactiveElement.SetActive(true);
        Debug.Assert(interactiveElement.activeSelf, "Tooltip should be active after OnHoverEnter.");
        activeTooltip = interactiveElement;
    }

    /// <summary>
    /// Hides the tooltip when hover exits.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `interactiveElement` must be initialized.
    /// Postconditions:
    /// - If this tooltip is still the active one, it is hidden.
    /// </remarks>
    private void OnHoverExit()
    {
        //null check
        if (interactiveElement == null)
        {
            Debug.LogError("OnHoverExit called but interactiveElement is null.");
        }
       // Only hide prev tooltip, not current one also
        if (activeTooltip == interactiveElement)
        {
            interactiveElement.SetActive(false);
            Debug.Assert(!interactiveElement.activeSelf, "Tooltip should be inactive after OnHoverExit.");
            activeTooltip = null;
        }
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
            trigger.HoverEntered -= OnHoverEnter;
            trigger.HoverExited -= OnHoverExit;
        }

         // If this tooltip is still the active one, hide it
        if (interactiveElement != null && activeTooltip == interactiveElement)
        {
            interactiveElement.SetActive(false);
            activeTooltip = null;
        }
    }
}
