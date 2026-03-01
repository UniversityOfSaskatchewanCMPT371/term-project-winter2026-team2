using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    /// <preconditions>
    /// <c>interactiveElement</c> and <c>trigger</c> must not be null.
    /// </preconditions>
    /// <postconditions>
    /// Event handlers are subscribed to trigger events and the interactive element is hidden.
    /// </postconditions>
    public ToolTipController(GameObject interactiveElement, IToolTipTrigger trigger)
    {
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
    /// <preconditions>
    /// <c>interactiveElement</c> must be initialized.
    /// </preconditions>
    /// <postconditions>
    /// The interactive element is visible and active.
    /// </postconditions>
    private void OnHoverEnter()
    {
        interactiveElement.SetActive(true);
    }

    /// <summary>
    /// Hides the tooltip when hover exits.
    /// </summary>
    /// <preconditions>
    /// <c>interactiveElement</c> must be initialized.
    /// </preconditions>
    /// <postconditions>
    /// The interactive element is hidden and inactive.
    /// </postconditions>
    private void OnHoverExit()
    {
        interactiveElement.SetActive(false);
    }

    /// <summary>
    /// Cleans up event subscriptions to prevent memory leaks.
    /// </summary>
    /// <preconditions>
    /// Event handlers must be subscribed to trigger events.
    /// </preconditions>
    /// <postconditions>
    /// All event handlers are unsubscribed from trigger events.
    /// </postconditions>
    public void Dispose()
    {
        //unsubscribe from the trigger events to prevent memory leaks
        trigger.HoverEntered -= OnHoverEnter;
        trigger.HoverExited -= OnHoverExit;
    }
}
