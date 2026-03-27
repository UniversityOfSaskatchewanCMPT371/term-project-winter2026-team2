using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of RotateButtonView.
/// </summary>
public class RotateButtonView : View<IRotateButtonController>, IRotateButtonView
{
    // use 'this.controllerInstance' to access controller component

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();

        SetupXREvents();
    }

    /// <inheritdoc/>
    public void SetupXREvents()
    {
        if (controllerInstance == null)
        {
            Debug.LogWarning("Controller instance cannot be null on XR events setup");
        }
        Assert.IsNotNull(controllerInstance, "Controller must not be null on XR events setup");

        var components = GetComponentsInChildren<XRBaseInteractable>();
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
            c.selectEntered.AddListener(OnXRClick);
            Assert.IsNotNull(c, "Failed to add XR events to a (null) component");
        }
    }

    private void OnXRClick(SelectEnterEventArgs args)
    {
        controllerInstance.OnButtonPressed();
    }

}
