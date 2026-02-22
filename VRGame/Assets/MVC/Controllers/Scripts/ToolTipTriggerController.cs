using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolTipController
{
    private readonly GameObject interactiveElement;
    private readonly IToolTipTrigger trigger;

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

    private void OnHoverEnter()
    {
        interactiveElement.SetActive(true);
    }

    private void OnHoverExit()
    {
        interactiveElement.SetActive(false);
    }

    public void Dispose()
    {
        //unsubscribe from the trigger events to prevent memory leaks
        trigger.HoverEntered -= OnHoverEnter;
        trigger.HoverExited -= OnHoverExit;
    }
}
