using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of CheckButton.
/// Sets up XR event listeners for button press 
/// </summary>
public class CheckButtonView : View<ICheckButtonController>, ICheckButtonView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();
        SetupXREvents();
    }

    /// <inheritdoc/>
    public void SetupXREvents()
    {
        Assert.IsNotNull(controllerInstance, "Controller must not be null on XR events setup");

        // Find all XRBaseInteractable components in children and add events
        var components = GetComponentsInChildren<XRBaseInteractable>();
        if (components.Length == 0)
            Debug.LogWarning("No XRBaseInteractable components found on CheckButton.");

        foreach (var c in components)
        {
            Assert.IsNotNull(c, "Null component found in XRBaseInteractable components");
            c.selectEntered.AddListener(OnXRClick);
        }
    }

    /// <inheritdoc/>
    private void OnXRClick(SelectEnterEventArgs args)
    {
        controllerInstance.OnButtonPressed();
    }
}
