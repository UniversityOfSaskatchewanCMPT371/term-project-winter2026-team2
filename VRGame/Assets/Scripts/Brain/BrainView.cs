using UnityEngine;

/// <summary>
/// View component of BrainView.
/// </summary>
public class BrainView : View<IBrainController>, IBrainView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();
    }

    /// <inheritdoc>
    public void SetupXREvents()
    {
        // Get children components
        var children = GetComponentsInChildren<XRBaseInteractable>();

        foreach (var child in children)
        {
            child.hoverEntered.AddListener(OnXRHoverEnter);
            child.hoverExited.AddListener(OnXRHoverExit);
        }

    }

    /// <inheritdoc>
    private void OnXRHoverEnter(HoverEnterEventArgs args)
    {
        controllerInstance.OnHoverEnter();
    }

    /// <inheritdoc>
    private void OnXRHoverExit(HoverExitEventArgs args)
    {
        controllerInstance.OnHoverExit();
    }

}

