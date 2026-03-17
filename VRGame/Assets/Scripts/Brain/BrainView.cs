using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of BrainView.
/// </summary>
public class BrainView : View<IBrainController>, IBrainView
{
    /// <inheritdoc/>
    public override void Init()
    {
        if (controllerInstance != null)
        {
            Debug.LogWarning("Controller instance already exists");
        }
        Assert.IsNull(controllerInstance, "Controller instance must be null prior to initialize");
        this.CheckControllerRef();
        Assert.IsNotNull(controllerInstance, "Controller failed to initialize in BrainView");
        SetupXREvents();
    }

    /// <inheritdoc>
    public void SetupXREvents()
    {
        if (controllerInstance == null)
        {
            Debug.LogWarning("Controller instance cannot be null on XR events setup");
        }
        Assert.IsNotNull(controllerInstance, "Controller must Not be null on XR events setup");
        var components = GetComponentsInChildren<XRBaseInteractable>();
        // Assign listeners on each component
        if (components.Length == 0)
        {
            Debug.LogWarning("Cannot get components, none exist");
        }
        foreach (var c in components)
        {
            Assert.IsNotNull(c, "Null component found in components");
            if (c == null)
            {
                Debug.LogError("Null component detected");
            }
            c.hoverEntered.AddListener(OnXRHoverEnter);
            c.hoverExited.AddListener(OnXRHoverExit);
            Assert.IsNotNull(c, "Failed to add XR events to a (null) component");
        }

    }

    /// <inheritdoc>
    private void OnXRHoverEnter(HoverEnterEventArgs args)
    {
        if (controllerInstance == null)
        {
            Debug.LogError("Controller instance is null on XRHoverEnter()");
        }
        Assert.IsNotNull(controllerInstance, "Controller instance must exist OnXRHoverEnter()");
        controllerInstance.OnHoverEnter();
    }

    /// <inheritdoc>
    private void OnXRHoverExit(HoverExitEventArgs args)
    {
        if (controllerInstance == null)
        {
            Debug.LogError("Controller instance is null on XRHoverExit()");
        }
        Assert.IsNotNull(controllerInstance, "Controller instance must exist OnXRHoverExit()");
        controllerInstance.OnHoverExit();
    }

}

