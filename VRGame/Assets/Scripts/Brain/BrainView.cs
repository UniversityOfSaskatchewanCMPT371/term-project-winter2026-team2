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

