using UnityEngine;
using UnityEngine.Assertions;

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
        this.CheckControllerRef();
        Assert.IsNotNull(controllerInstance, "Controller failed to initialize in BrainView");
    }

    /// <inheritdoc>
    public void SetupXREvents()
    {
        var components = GetComponentsInChildren<XRBaseInteractable>();
        // Assign listeners on each component
        if (components.Length == 0)
        {
            Debug.LogWarning("Cannot get components, none exist");
        }
        foreach (var c in components)
        {
            if (c == null)
            {
                Debug.LogWarning("Null component detected");
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

